using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class DataFarmer {

    // 
    private static DataFarmer me = null;

    // 
    private List<IDataFarmerObject> data = new List<IDataFarmerObject>();

    // SET BUFFER THRESHHOLD WHICH, WHEN MET, WILL STREAM DATA CHUNKS OUTWARD
    private static int BUFFER_FULL = 10;

    // Establish Webclient
    private WebClient webClient;

    // Declare authorization code
    private string auth;

    // Declare Participant Subject ID
    private long participant = -1;


    // TODO: put this stuff in a config file
    // This is a webservice that is now running - i.e. we can save data remotely
    private static string REMOTE_URI = "http://cslab.psyc.sfu.ca:13524";
    private static string REMOTE_SECRET = "doorcode";


    // Use this for initialization - Creates a "virtual" game object.
	public static DataFarmer GetInstance () {
		if (me == null)
        {
            me = new DataFarmer();
        }
        return me;
	}

	private DataFarmer()
    {
        using (webClient = new WebClient())
        {
            
            
            // example of getting a participant id automatically
            // TODO: grab a human entered participant id
            // you can just log in with /login/<key> rather than /new/<key>
            // if you are already logged in use /new to get a new part id
            // the auth cookie must be added for the /new syntax to work
            string content = webClient.DownloadString(string.Format("{0}/new/{1}", REMOTE_URI, REMOTE_SECRET));
            try
            {
                long.TryParse(content, out participant);
                Debug.Log(string.Format("participant {0}", participant));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            
            
            
            // TODO: there is probably an easier way to do this - I'm not sure what this seciton is aiming to do.
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
            // end web connection?
            webClient.Dispose();
        }

    }

    public string getParticipantAsString()
    {
        return participant.ToString();
    }

    public long getParticipant()
    {
        return participant;
    }

    // Saving the Data Chunk to File
	public void Save (IDataFarmerObject thingToSave) {
        
        // Since this section is called every frame, the data.add line will continue to receive a new line of data on every iteration until the BUFFER is reached
        data.Add(thingToSave);
        
        if (data.Count == BUFFER_FULL)
        {

            // Serialize data structure
            string dataString = "";
            foreach (IDataFarmerObject o in data)
            {
                dataString += o.Serialize(participant);
            }

            // Report in console that data has been moved, and then update csv on file path
            Debug.Log("Data Moving to File!");
            using (StreamWriter file =
                   File.AppendText(@"C:\Users\CSLUser\Desktop\df.csv"))
            {
                file.Write(dataString);
            }
            try
            {
                // TODO: don't just keep failing if the cookie is no longer valid
                // TODO: put all this webclient code in another class/method
                // TODO: look at more sophisticated ways to to the requests e.g. https://social.msdn.microsoft.com/Forums/vstudio/en-US/33798503-2896-4850-aa4b-11022c2b3adf/how-can-i-send-webrequest-with-multiple-cookies?forum=csharpgeneral
                if (participant > 0)
                {
                    using (webClient = new WebClient())
                    {
                        webClient.Headers.Add("Content-Type", "text/plain");
                        webClient.Headers.Add("Cookie", auth);
                        string uri = string.Format("{0}/save/{1}", REMOTE_URI, participant);
                        string result = webClient.UploadString(uri, "POST", dataString);
                        Debug.Log("save result: " + result);
                    }
                    webClient.Dispose();
                }
            } 
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            data.Clear();
        }
	}
}
