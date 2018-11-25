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
        // Cal: we should definitely complain and abort if the id doesn't show up
        // as that would mean we lost our connection to mary
        participantID.text = DataFarmer.GetInstance().GetParticipantAsString();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void action()
    {
        camVR.GetComponent<CustomTag>().setTag(0, participantID.text);
        DataFarmer.GetInstance().SetParticipant(participantID.text);
        menu.SetActive(false);
    }

}
