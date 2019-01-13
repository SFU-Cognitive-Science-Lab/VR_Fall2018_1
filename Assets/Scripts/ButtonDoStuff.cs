using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonDoStuff : MonoBehaviour {

    public Button apply;
    public InputField participantID;
    public Camera camVR;
    public GameObject menu;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    // Use this for initialization
    void Start () {
        participantID.text = ps.GetParticipantAsString();
    }

    // in order for this to work we need an EventSystem component 
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ps.GetParticipant() > 0)
            {
                menu.SetActive(false);
                ps.ConditionFromParticipant();
                Debug.Log("condition " + ps.GetCondition() + " for " + ps.GetParticipant());
                SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            }
        }
    }

}
