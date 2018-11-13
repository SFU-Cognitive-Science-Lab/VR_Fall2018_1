using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
class DFPositionRotation : DataFarmerObject {
    public float timestamp;
    public Vector3 position;
    public Quaternion rotation;

    public DFPositionRotation(float timestamp, Vector3 position, Quaternion rotation)
    {
        this.timestamp = timestamp;
        this.position = position;
        this.rotation = rotation;
    }

    public override string Serialize(long participant)
    {
        return string.Format("posrot,{0},{1},{2},{3},{4},{5},{6},{7}\n", 
            participant, timestamp, position.x, position.y, position.z, 
            rotation.x,rotation.y,rotation.z);
    }
 }
