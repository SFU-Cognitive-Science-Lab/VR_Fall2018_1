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
        // TODO Cal: we should definitely complain and abort if the id doesn't show up
        // as that would mean we lost our connection to mary
        participantID.text = DataFarmer.GetInstance().GetParticipantAsString();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void action()
    {
        camVR.GetComponent<CustomTag>().setTag(0, participantID.text);
        
        // Cal: this was being set in the Cubes.createSet() method but we may want to
        // simply enter this at the start of experiment rather than depend on the 
        // participant id - which could get out of sync if we have multiple false starts
        applicator.Condition = 
            DataFarmer.GetInstance().SetParticipant(participantID.text).ConditionFromParticipant();

        menu.SetActive(false);
    }

}
