using UnityEngine;

public class DataFarmerObject : IDataFarmerObject
{
    readonly protected string tag;
    protected float timestamp;
    public long participant;
    public long trial;
    public int condition;
    public string category;
    public Transform cube;

    public DataFarmerObject(string tag)
    {
        this.tag = tag;
        this.timestamp = Time.time;
        this.participant = DataFarmer.GetInstance().GetParticipant();
        this.trial = DataFarmer.GetInstance().GetTrial();
        this.cube = DataFarmer.GetInstance().GetCube();
        this.condition = DataFarmer.GetInstance().GetCondition();
        this.category = DataFarmer.GetInstance().GetCategory();
    }
    public float GetTimestamp()
    {
        return timestamp;
    }
    public string GetTag()
    {
        return tag;
    }
    public long GetTrial()
    {
        return this.trial;
    }
    public long GetParticipant()
    {
        return this.participant;
    }
    public virtual string Serialize()
    {
        string cubetag = "";
        if (cube != null)
        {
            cubetag = cube.name;
        }
        return string.Format("{0},{1},{2},{3},{4},{5},{6}", 
            tag, participant, timestamp, condition, trial, cubetag, category);
    }
}