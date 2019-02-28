using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTrial : MonoBehaviour {
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    public void Start()
    {
        ps.IncTrial();
    }

    public void OnTriggerStay(Collider other)
    {
        if (leftController.controller.GetHairTriggerDown() || rightController.controller.GetHairTriggerDown())
        {
            // check if the participant made an answer then move on to next trial
            if (ps.ChoiceMade())
            {
                ps.IncTrial();
                if (ps.IsFinished())
                {
                    SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
                }
                Debug.Log(string.Format("Starting trial {0} at {1}", ps.GetTrial(), Time.time));
            }
        }
    }
}
