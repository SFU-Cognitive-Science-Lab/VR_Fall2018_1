using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestSide : MonoBehaviour {
    GameObject player;
    string[] dirStrings = { "up", "right", "forward" };
    int previousNearestFace;
    bool detailedLogging = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Player", player);
        previousNearestFace = -1;
        detailedLogging = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            GameObject cube = GameObject.FindGameObjectWithTag("Interactable Object");
            if (cube != null)
            {
                Transform transform = cube.GetComponent<Transform>();
                Vector3[] directions = { transform.up, transform.right, transform.forward };
                float minAngle = 90.0F;
                int nearestFace = 0;
                int face = 0;
                foreach (Vector3 direction in directions)
                {
                    float angle = Vector3.Angle(player.transform.forward, direction);
                    if (detailedLogging) { 
                        Debug.Log("testing face " + dirStrings[face] + " angle " + angle + " min " + minAngle + " min face " + dirStrings[nearestFace]);
                    }
                    if (angle < minAngle) { minAngle = angle; nearestFace = face; }
                    face++;
                }
                if (previousNearestFace != nearestFace)
                {
                    Debug.Log(dirStrings[nearestFace] + " (" + nearestFace + ") is closest.");
                    previousNearestFace = nearestFace;
                }
            }
        }
	}
}
