using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// here we save everything that relates to the participant during a test run
// when a DataFarmerObject is saved this data is used to flesh out the context
// of the measurement
public class ParticipantStatus
{
    public static readonly long NO_PARTICIPANT = -1;
    public static readonly int NO_ARRANGEMENT = -1;

    // Participant Subject ID initially garnered from the external host
    // can also be set from the UI when the run starts
    // not having this set to a sensible value is a show stopper
    // we really should not be continuing in that case
    // TODO: figure out best way to kill the run if there are conf errors
    //       maybe we should simply not start until this is set?
    private long participant = NO_PARTICIPANT;

    // trial == iteration of learning using a randomized cube
    // run == group of trials for a participant
    private long trial = 0;

    // answers = category chosen by participant at each trial
    private SortedDictionary<long, string> answers = new SortedDictionary<long, string>();

    // condition == which set of cubes -> categories was chosen
    // this affects overall what the participant sees during the experiment
    // we need to check this to ensure counterbalancing is correct
    private Condition condition = null;

    // which cube got invoked for a given learning trial
    private Transform cube;

    // what answer they chose for that trial
    private string choice;

    // how far they've moved
    private Dictionary<string, KeyValuePair<float, float>> displacements;

    // we assume only one participant per run hence we can instantiate ourselves here
    private static ParticipantStatus ps = new ParticipantStatus();

    public static ParticipantStatus GetInstance()
    {
        return ps;
    }

    // instead of doing everything with static methods
    // leave the door open for configuring ourselves similar to how DataFarmer does it
    // if we use the config file here we should make a separate configuration module
    private ParticipantStatus()
    {
        displacements = new Dictionary<string, KeyValuePair<float, float>>();
    }

    // this is mainly used when we get the answer during a trial to make code simpler
    public DataFarmer GetDataFarmer()
    {
        return DataFarmer.GetInstance();
    }

    // use this method when setting the participant id by hand
    public ParticipantStatus SetParticipant(string part)
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

    // used in the UI where the participant id can be changed
    public string GetParticipantAsString()
    {
        return GetParticipant().ToString();
    }

    // in the UI we don't use DataFarmer directly 
    // hence we bootstrap a new ID if the participant isn't set
    public long GetParticipant()
    {
        if (participant == NO_PARTICIPANT)
        {
            SetParticipant(GetDataFarmer().NewParticipant());
        }
        return participant;
    }

    // set the counterbalancing condition for this participant
    public void SetArrangement(int cubeset, int arrangement)
    {
        this.condition = new Condition(cubeset, arrangement);
    }

    // gets the arrangement for this participant
    public Condition GetArrangement()
    {
        return this.condition;
    }

    // idea with the set methods is that they can be stringed together
    // ParticipantStatus.GetInstance().SetThing1(thing1).SetThing2(thing2)...
    public ParticipantStatus SetTrial(long trial)
    {
        if (trial > 0) this.trial = trial;
        return this;
    }

    public ParticipantStatus IncTrial()
    {
        this.trial++;
        return this;
    }

    public long GetTrial()
    {
        return this.trial;
    }

    public ParticipantStatus SetCondition(int cubeset, int arrangement)
    {
        var cl = GetDataFarmer().CubeLists;

        if (cubeset < 0 || cubeset >= cl.CountCubesets())
            throw new ArgumentException("cubeset is out of bounds");

        if (arrangement < 0 || arrangement >= cl.CountArrangements(cubeset))
            throw new ArgumentException("arrangement is out of bounds");

        this.condition = new Condition(cubeset, arrangement);
        return this;
    }

    // Convenience methods for getting descriptions of cubes:
    // a run is a series of trials that iterate through a list of cubes
    // when we get the end of a list we generate another 
    private List<CubeTuple> Cubes;
    private int Cube = -1;
    public CubeTuple GetNextStimulus()
    {
        Cube++;
        if (Cubes == null || Cube == Cubes.Count)
        {
            ResetCubes();
            Cube = 0;
        }
        return Cubes[Cube];
    }

    // make a random permutation of a given list of cubes
    private void ResetCubes()
    {
        if (this.condition == null)
            throw new ArgumentException("Need to set a condition for this participant!");
        Cubes = GetDataFarmer().CubeLists.Shuffle(this.condition);
    }

    // used in the UI to set the set of cube/category mappings the participant will be learning
    public Condition ConditionFromParticipant()
    {
        var cl = GetDataFarmer().CubeLists;
        int cubeset = (int)(participant % cl.CountCubesets());
        int arrangement = (int) (participant % cl.CountArrangements(cubeset));

        this.condition = new Condition(cubeset, arrangement);

        return this.condition;
    }

    public Condition GetCondition()
    {
        return this.condition;
    }

    // See ChoiceBehavior.cs for examples of how this is used
    public string GetCategory()
    {
        if (Cube < 0) return "";
        if (Cubes == null) return "";
        return Cubes[Cube].GetCategory();
    }

    // used by the DistanceTravelled.cs script to save the displacements of something
    public void UpdateDisplacement(string tag, float displacement)
    {
        var TimeVsDist = new KeyValuePair<float, float>(Time.time, displacement);
        if (displacements.ContainsKey(tag))
        {
            displacements[tag] = TimeVsDist;
        }
        else
        {
            displacements.Add(tag, TimeVsDist);
        }
    }

    // create a string that we can save of the last saved displacements
    public string DisplacementsToString()
    {
        var sb = new StringBuilder();
        foreach (var kv in displacements)
        {
            // probably want to think of a better way to present this information
            sb.AppendFormat("{0}={1}/{2},", kv.Key, kv.Value.Value, kv.Value.Key);
        }
        return sb.ToString();
    }

    // this assumes we've had at least one trial and will only accept the first answer
    public bool SetChoice(string ans)
    {
        if (answers.ContainsKey(trial)) return false;
        answers.Add(trial, ans);
        return true;
    }

    public string GetLastChoice()
    {
        if (trial > 0) return answers[trial];
        return null;
    }

    public string GetTrialChoice(long t)
    {
        if (answers.ContainsKey(t)) return answers[t];
        return null;
    }

    // the condition for this experiment is the set of cubes
    // each participant should be evenly dispersed between the different 
    // possible mappings of the cubes to categories
    public class Condition {
        public int cubeset { get; set;  } // there are only a small number of ways to generate groups of distinct cubes
        public int arrangement { get; set;  } // out of these we should select one mapping of cubes to categories

        public Condition(int c, int a)
        {
            cubeset = c;
            arrangement = a;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", cubeset, arrangement);
        }
    }
}
