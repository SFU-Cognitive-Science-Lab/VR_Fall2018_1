using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Movement : MonoBehaviour
{

    private Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;

    EVRButtonId hairTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;

    // Use this for initialization
    void Start()
    {
        //leftController = GetComponent<Hand>()
        //Debug.Log("Look out Below");
        //Debug.Log(GetComponent<Hand>());

        leftController = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
        Debug.Log(leftController);
    }

    // Update is called once per frame
    void Update()
    {
        /* var triggerVector = player.rightController.GetAxis(hairTrigger);
         if (triggerVector.x == 1)// trigger has a value from 0 to 1;
         {
             "do something"
         }*/
        GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");
        //Debug.Log(currentObjs[0].transform.rotation);
    }

    public void OnTriggerStay(Collider other)
    {

        if (leftController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) || rightController.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("WORKING");


        }
    }
}
