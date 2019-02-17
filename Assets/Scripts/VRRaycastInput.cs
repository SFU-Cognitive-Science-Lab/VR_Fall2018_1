using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRaycastInput : MonoBehaviour {

    void Awake()
    {
        //If attached to head, will return null (expected behavior)
        //I'm using this method to differentiate between head and hands
        //controllerInput = GetComponent();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
