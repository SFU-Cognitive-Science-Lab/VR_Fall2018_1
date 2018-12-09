﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class DataFarmer {

    // 
    private static DataFarmer me = null;

    // buffer for incoming data
    private List<IDataFarmerObject> data = new List<IDataFarmerObject>();

    // SET BUFFER THRESHHOLD WHICH, WHEN MET, WILL STREAM DATA CHUNKS OUTWARD
    // this can be overridden from the config file
    private static int BUFFER_FULL = 10;
    private static int SAVE_RETRIES = 5;

    // Cal: some state variables. TODO put this stuff in a separate class...

    // trial == iteration of learning using a randomized cube
    private long trial = 0;

    // condition == which set of cubes -> categories was chosen
    // this affects overall what the participant sees during the experiment
    // we need to check this to ensure counterbalancing is correct
    // we assume we know what this map is (maybe it doesn't matter?)
    private int condition;

    // which cube got invoked
    private Transform cube;

    // what they chose
    private string choice;

    // Webclient needed to save data externally
    private WebClient webClient;

    // Declare authorization code
    private string auth;

    // We started with this state variable which we get externally
    // Participant Subject ID initially garnered from the external host
    // can be overridden from the UI at the moment
    private long participant = -1;

    // Cal: end of state variables

    // must be true if we are to save data externally
    private bool loggedin = false;

    // for GetConfig below
    private static string CONFIG_FILE;
    private static string REMOTE_URI;
    private static string REMOTE_SECRET;
    private static string LOCAL_LOG;

    // Use this for initialization - Creates a "virtual" game object.
    public static DataFarmer GetInstance () {
		if (me == null)
        {
            me = new DataFarmer();
        }
        return me;
	}

    // this particular instantiation assumes we are logging to an 
    // external server that is coordinating participant ids
	private DataFarmer()
    {
        GetConfig();
        if (Login())
        {
            NewParticipant();
        }
    }

    // use saved configuration data to 
    private static void GetConfig()
    {
        if (CONFIG_FILE == null)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CONFIG_FILE = string.Format(@"{0}\{1}", desktop, "experiments.config.txt");
        }
        Regex linePat = new Regex(@"(?<name>\w+)[=:\s](?<value>.*)");
        using (StreamReader conf = File.OpenText(CONFIG_FILE))
        {
            while (!conf.EndOfStream)
            {
                string line = conf.ReadLine();
                if (line.StartsWith("#") || line.StartsWith("//"))
                {
                    continue;
                }
                Match lineMatch = linePat.Match(line);
                if (!lineMatch.Success)
                {
                    continue;
                }
                string name = lineMatch.Groups["name"].ToString().ToLower();
                string value = Regex.Replace(lineMatch.Groups["value"].ToString(), @"\r\n?|\n", "");
                switch (name)
                {
                    case "url": case "uri": REMOTE_URI = value; break;
                    case "secret": case "password": REMOTE_SECRET = value; break;
                    case "log": LOCAL_LOG = value; break;
                    case "buffer": BUFFER_FULL = int.Parse(value); break;
                    default: Debug.Log(string.Format("don't know what {0} is", name)); continue;
                }
                Debug.Log(string.Format("set '{0}' to '{1}'.\n", name, value));
            }
        }
    }

    // use this method to reset the configuration file before 
    public DataFarmer SetConfigFile(string fname)
    {
        CONFIG_FILE = fname;
        Debug.Log("reset config file to " + CONFIG_FILE);
        if (File.Exists(CONFIG_FILE))
        {
            Debug.Log("updating configuration");
            GetConfig();
        }
        return this;
    }

    // use this method when setting the participant id by hand
    public DataFarmer SetParticipant(string part)
    {
        Debug.Log("trying to set participant id to " + part);
        try
        {
            long.TryParse(part, out participant);
            Debug.Log("participant id now " + participant);
        }
        catch (Exception e)
        {
            participant = -1;
            Debug.Log(string.Format("error setting participant {0}: {1}: {2}", part, e, e.Message));
        }
        return this;
    }

    public string GetParticipantAsString()
    {
        return participant.ToString();
    }

    public long GetParticipant()
    {
        return participant;
    }

    // Cal: TODO this stuff could be put in a different class 
    // idea with the set methods is that they can be stringed together
    // DataFarmer.GetInstance().SetThing1(thing1).SetThing2(thing2)...
    public DataFarmer SetTrial(long trial)
    {
        if (trial > 0) this.trial = trial;
        return this;
    }

    public DataFarmer IncTrial()
    {
        this.trial++;
        return this;
    }

    public long GetTrial()
    {
        return this.trial;
    }

    public DataFarmer SetCondition(int condition)
    {
        this.condition = condition;
        return this;
    }
    
    public int ConditionFromParticipant()
    {
        this.condition = (int)(participant % applicator.ConditionCount);
        return this.condition;
    }

    public int GetCondition()
    {
        return this.condition;
    }

    public DataFarmer SetCube(Transform cube)
    {
        this.cube = cube;
        return this;
    }

    public Transform GetCube()
    {
        return this.cube;
    }

    // See ChoiceBehavior.cs for examples of how this is used
    public string GetCategory()
    {
        if (cube == null) return "";
        return cube.GetComponent<CustomTag>().getTag(0).Substring(0,1);
    }

    // Because the ChoiceBehavior.cs script is getting invoked many times, 
    // keep the choice multiple times but report it once
    public DataFarmer SetChoice(string choice)
    {
        this.choice = choice;
        return this;
    }

    public string GetChoice()
    {
        return this.choice;
    }

    // logs in and as a side effect gets our auth cookie - needed for all other requests
    private bool Login()
    {
        loggedin = false;
        if (REMOTE_URI == null || REMOTE_SECRET == null)
        {
            throw new Exception("DataFarmer missing configuation needed to log in!");
        }
        using (webClient = GetNewWebClient())
        {
            string content = webClient.DownloadString(makeNonce().Uri(string.Format("{0}/login", REMOTE_URI)));
            Debug.Log("login result: " + content);
            if (content.Contains("OK"))
            {
                loggedin = true;
            }
            WebHeaderCollection headers = webClient.ResponseHeaders;
            int i = 0;
            for (; i < headers.Count; i++)
            {
                Debug.Log(headers.GetKey(i) + ": " + headers.Get(i));
                if (headers.GetKey(i) == "Set-Cookie")
                {
                    auth = headers.Get(i);
                    Debug.Log(string.Format("{0}", auth));
                    break;
                }
            }
            webClient.Dispose();
        }
        return loggedin;
    }

    // tools for making basic requests - we must be logged in to use them
    // Post uses the http POST method and expects extra data to send
    private string PostRequest(string uri, string dataString)
    {
        string result = null;
        using (webClient = GetNewWebClient())
        {
            // if this auth cookie is invalid nothing will work ...
            webClient.Headers.Add("Content-Type", "text/plain");
            webClient.Headers.Add("Cookie", auth);
            result = webClient.UploadString(uri, "POST", dataString);
        }
        webClient.Dispose();
        return result;
    }

    // Get uses http's GET method and only needs a url
    private string GetRequest(string uri)
    {
        string result = null;
        using (webClient = GetNewWebClient())
        {
            webClient.Headers.Add("Content-Type", "text/plain");
            webClient.Headers.Add("Cookie", auth);
            result = webClient.DownloadString(uri);
        }
        webClient.Dispose();
        return result;
    }

    // make a participant id if we don't have one already
    private void NewParticipant()
    {
        string content = GetRequest(string.Format("{0}/new", REMOTE_URI));
        try
        {
            long.TryParse(content, out participant);
        }
        catch (Exception e)
        {
            Debug.Log(string.Format("error getting new participant id: {0}: {1}", e, e.Message));
        }
    }

    // Saving the Data Chunk to File and remotely
    // on error does a lot of complaining and will throw an exception if no data can be saved
    public void Save (DataFarmerObject thingToSave) {
        
        // Since this section is called every frame, the data.add line will continue to receive a new line of data on every iteration until the BUFFER is reached
        data.Add(thingToSave);

        if (data.Count == BUFFER_FULL)
        {
            bool anythingsaved = false;
            // Serialize data structure
            string dataString = "";
            foreach (IDataFarmerObject o in data)
            {
                dataString += o.Serialize();
            }

            // update csv log on file path
            if (LOCAL_LOG != null)
            {
                try
                {
                    Debug.Log("Data Moving to File!");
                    using (StreamWriter file = File.AppendText(LOCAL_LOG))
                    {
                        file.Write(dataString);
                        anythingsaved = true;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(string.Format("failed to write to log: {0} {1}", e, e.Message));
                }
            }

            if (SaveRemotely(dataString))
            {
                anythingsaved = true;
            }

            data.Clear();
            if (!anythingsaved)
            {
                Debug.Log("ERROR: NO DATA COULD BE SAVED!");
                throw new Exception("no data can be saved");
            }
        }
	}

    // send remote data if we can
    private bool SaveRemotely(string dataString)
    {
        bool saved = false;
        try
        {
            if (!loggedin) Login();

            if (loggedin && participant >= 0)
            {
                // check if we succeeded and retry a limited number of times with backoff if we don't
                int tries = SAVE_RETRIES;
                do
                {
                    saved = false;
                    string uri = string.Format("{0}/save/{1}", REMOTE_URI, participant);
                    string result = PostRequest(uri, dataString);
                    Debug.Log("save result: " + result);
                    if (result.StartsWith("OK"))
                    {
                        Debug.Log("confirmed save");
                        saved = true;
                    }
                    else
                    {
                        tries--;
                        if (tries <= 0) break;
                        Debug.Log("failed trying to log in again " + tries + " tries left");
                        Thread.Sleep(SAVE_RETRIES - tries * 500); // ms to wait before trying again
                        Login();
                    }
                } while (loggedin && !saved);
                if (!saved)
                {
                    Debug.Log("ERROR: NOT ABLE TO SAVE DATA REMOTELY!");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return saved;
    }
    // gets a very tolerant web client 
    private WebClient GetNewWebClient()
    {
        // this line is needed for https to work with our self-signed certificates
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        return new WebClient();
    }

    // how to hide sending the actual password over the network
    private Nonce makeNonce()
    {
        return new Nonce(REMOTE_SECRET);
    }

    // does some fancy stuff to use a random number to obscure our password
    // this class is effectively final: there is no way to change the code/nonce after instantiation
    private class Nonce
    {
        private int nonce;
        private string code;

        public Nonce(string secret)
        {
            System.Random random = new System.Random();
            nonce = random.Next(int.MaxValue);
            code = Encode(secret);
        }

        // another architecture would be to have MakeCode be a callback
        private string Encode(string secret)
        { 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(MakeCode(secret)));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                code = builder.ToString();
            }
            return code;
        }

        // this can be made more complex but has to also be replicated on the remote server
        private string MakeCode(string secret)
        {
            return string.Format("{0}{1}", nonce, secret);
        }

        public string Uri(string baseUri)
        {
            return string.Format("{0}/{1}/{2}", baseUri, code, nonce);
        }
    }
}
