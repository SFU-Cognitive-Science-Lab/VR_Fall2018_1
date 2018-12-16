using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrial : MonoBehaviour {
    private ParticipantStatus ps;

	// Use this for initialization
	void Start ()
    {
        ps = ParticipantStatus.GetInstance();
        ps.IncTrial();
	}
	
	
	public void OnTriggerExit(Collider other)
    {
        ps.IncTrial();
        Debug.Log(string.Format("Starting trial {0} at {1}", ps.GetTrial(), Time.time));
	}
}
