using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAnswer : MonoBehaviour {
    private DataFarmer farmer;

	// Use this for initialization
	void Start ()
    {
        farmer = DataFarmer.GetInstance();
        farmer.IncTrial();
	}
	
	
	public void OnTriggerExit(Collider other)
    {
        farmer.Save(new DFAnswerSelection());
        farmer.IncTrial();
	}
}
