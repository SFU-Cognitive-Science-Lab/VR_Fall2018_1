public abstract class DataFarmerObject : IDataFarmerObject
{
    // this variable tracks our current timestamp since intialization and serializes the participant value
    readonly protected string tag;
    protected float timestamp;
    public abstract string Serialize(long participant);

    public DataFarmerObject(string tag, float timestamp)
    {
        this.tag = tag;
        this.timestamp = timestamp;
    }
    public float GetTimestamp()
    {
        return timestamp;
    }
    public string GetTag()
    {
        return tag;
    }
}