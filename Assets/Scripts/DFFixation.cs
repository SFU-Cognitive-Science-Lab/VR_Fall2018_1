using UnityEngine;

public class DFFixation : DataFarmerObject {
    private string cube;
    private string side;
    private string pointingAt;
    // this is just the head, left and right controller cumulative movement
    Vector3 movement;

	public DFFixation(float timestamp, string cube, string side, string pointingAt, Vector3 mv): 
        base("fixation", timestamp)
    {
        this.cube = cube;
        this.side = side;
        this.pointingAt = pointingAt;
        this.movement = mv;
    }

    public override string Serialize(long participant)
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\n", 
                tag, participant, timestamp, 
                cube, side, pointingAt, 
                movement.x, movement.y, movement.z);
    }
}
