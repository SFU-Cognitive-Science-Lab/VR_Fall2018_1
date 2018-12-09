using UnityEngine;

public class DFFixation : DataFarmerObject {
    private string side;
    private int up;
    private int forward;
    private int right;
    private string pointingAt;

	public DFFixation(int up, int forward, int right, string side, string pointingAt): 
        base("fixation")
    {
        this.side = side;
        this.pointingAt = pointingAt;
        this.up = up;
        this.forward = forward;
        this.right = right;
    }

    public override string Serialize()
    {
        return string.Format("{0},{1},{2},{3},{4},{5}\n",
                base.Serialize(), up, forward, right, side, pointingAt);
    }
}
