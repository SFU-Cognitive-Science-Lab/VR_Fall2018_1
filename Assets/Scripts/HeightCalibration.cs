using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightCalibration : MonoBehaviour {
    
    public GameObject[] buttons; //items has ABCD buttons, and next (in that order)
    public Transform cubespawn;
    GameObject p; //player head
    Vector3 py; //player height
    Vector3 buffer = new Vector3(0, 1.1f, 0);

    // Use this for initialization
    void Start () {
        p = GameObject.FindGameObjectWithTag("MainCamera"); //find player headset

        //grab only y-value from player position vector (second vector is norm to y-plane)
        py = Vector3.ProjectOnPlane(p.transform.position, Vector3.right);

        //subtract height so that a 'normal' resting height is considered as the zero point
        py -= buffer;

        //add the relative player height to object height
        for(int i = 0; i < 5; i++)
        {
            buttons[i].transform.position += py;
        }

        cubespawn.transform.position += py;

        Debug.Log(py);
        Debug.Log(cubespawn.transform.position);
    }
	
   
	// Update is called once per frame
	void Update () {
        //for all items, adjust height based on py
        
    }
}
