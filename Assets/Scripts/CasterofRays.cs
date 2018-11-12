using UnityEngine;
using System.Collections;

public class CasterofRays : MonoBehaviour
{
    // create a variable which allows us to specify which layers are interacted with
    public LayerMask mask;
    public string ObjUnderReticle;
    string HitInfoString;
    

    void Update()
    {
        // Updates the position of the player camera
        Ray ray = new Ray(transform.position, transform.forward);

        // Creates a variable for extracting info about the object hit by the ray
        RaycastHit HitInfo;

        // 4 layers to the raycast: when the ray goes out, the object hit must be within 100 meters, and must be on the specified layers ("mask")
        if (Physics.Raycast ( ray, out HitInfo, 100, mask))
        {
            // Extract info about the object hit - maybe feed this into csv or table from here.
           // print (HitInfo.collider.gameObject.name);
            Debug.DrawLine(ray.origin, HitInfo.point, Color.red);
            
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }

        HitInfoString = HitInfo.collider + "";
        ObjUnderReticle = HitInfoString;
        // Debug.Log(HitInfoString);
    }
}

//        // Bit shift the index of the layer (8) to get a bit mask
// int layerMask = 1 << 8;

// This would cast rays only against colliders in layer 8.
// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
// layerMask = ~layerMask;

// RaycastHit hit;
//        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
//        {
//            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
// Debug.Log("Did Hit");
//            Debug.Log(transform.name);
//        }
//        else
 //       {
 //           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
  //          Debug.Log("Did not Hit");
 //       }