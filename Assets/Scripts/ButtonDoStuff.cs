using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoStuff : MonoBehaviour {

    public Button apply;
    public InputField participantID;
    public Camera camVR;
    public GameObject menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void action()
    {
        camVR.GetComponent<CustomTag>().setTag(0, participantID.text);
        menu.SetActive(false);
    }

}
