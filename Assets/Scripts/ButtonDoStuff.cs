using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoStuff : MonoBehaviour {

    public Button apply;
    public InputField participantID;
    public Camera camVR;
    public GameObject menu;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    // Use this for initialization
    void Start () {
        // TODO Cal: we should definitely complain and abort if the id doesn't show up
        // as that would mean we lost our connection to our external host
        participantID.text = ps.GetParticipantAsString();
    }

    public void action()
    {
        camVR.GetComponent<CustomTag>().setTag(0, participantID.text);
        menu.SetActive(false);
        ps.ConditionFromParticipant();
        Debug.Log("condition " + ps.GetCondition() + " for " + ps.GetParticipant());
    }

}
