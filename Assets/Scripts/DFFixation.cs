using UnityEngine;

public class DFFixation : DataFarmerObject {
    private string cube;
    private string side;
    private string pointingAt;
    private string controllerTouching;

	public DFFixation(float timestamp, string cube, string side, string pointingAt, string controllerTouching): 
        base("fixation", timestamp)
    {
        this.cube = cube;
        this.side = side;
        this.pointingAt = pointingAt;
        this.controllerTouching = controllerTouching;
    }

    public override string Serialize(long participant)
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6}\n", 
                tag, participant, timestamp, cube, side, pointingAt, controllerTouching);
    }
}
