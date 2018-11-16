using UnityEngine;

class DFPositionRotation : DataFarmerObject {
    Vector3 position;
    Quaternion rotation;
    float distance; 

    public DFPositionRotation(float timestamp, Vector3 position, Quaternion rotation, float distanceTravelled):
        base("posrot", timestamp)
    {
        this.position = position;
        this.rotation = rotation;
        distance = distanceTravelled;
    }

    public override string Serialize(long participant)
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\n", 
            tag, participant, timestamp, position.x, position.y, position.z, 
            rotation.x,rotation.y,rotation.z, distance);
    }
 }
