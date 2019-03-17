using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColor : MonoBehaviour {
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    public Material[] materials;
    private int currentMaterial;

    // Use this for initialization
    void Start ()
    {
        currentMaterial = 0;
	}

    void OnTriggerStay(Collider other)
    {
        if (leftController.controller.GetHairTriggerUp() || rightController.controller.GetHairTriggerUp())
        {
            if (!TestState.isSelected())
            {
                TestState.SetSelectedTrue();
                currentMaterial = Random.Range(0, 1);
                if (currentMaterial >= materials.Length) currentMaterial = 0;
                gameObject.GetComponent<Renderer>().material = materials[currentMaterial];
            }
        }
    }
}
