using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrial : MonoBehaviour {
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

	public void OnTriggerExit(Collider other)
    {
        // check if the participant made an answer then move on to next trial
        // all this is now happening in ParticipantStatus.GetNextStimulus
        if (ps.ChoiceMade())
        {
            Debug.Log(string.Format("Starting trial {0} at {1}", ps.GetTrial(), Time.time));
        }
    }
}
