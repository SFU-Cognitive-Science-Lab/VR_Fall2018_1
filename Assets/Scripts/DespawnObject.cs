using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DespawnObject : MonoBehaviour {

    public Valve.VR.InteractionSystem.Hand fallbackController;
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;

    private List<Valve.VR.InteractionSystem.Hand> controllers;

    public bool destroyChildren;
    public bool destroyParent;
    public bool useTag;
    public string targetTag;

    // Use Start() for initialization.
    void Start()
    {
        controllers = new List<Valve.VR.InteractionSystem.Hand>();
        controllers.Add(fallbackController);
        controllers.Add(leftController);
        controllers.Add(rightController);
    }
	
    // Update() is called once per frame.
    void Update()
    {
    }

    // OnTriggerEnter() event listener.
    private void OnTriggerEnter(Collider other)
    {
        /*
        Transform otherTransform = other.transform;
        Transform parentTransform = otherTransform.parent;

        if (!useTag || (useTag && otherTransform.CompareTag(targetTag)))
        {
            detachControllerObjects(otherTransform, parentTransform);

            // Destroy Other Children
            if (destroyChildren)
            {
                foreach (Transform child in otherTransform)
                {
                    Destroy(child.gameObject);
                }
            }

            // Destroy Other
            Destroy(otherTransform.gameObject);

            // Destroy Other Parent
            if (destroyParent && parentTransform != null)
            {
                Destroy(parentTransform.gameObject);
            }
        }
        */
    }

    // Detach all attached controller objects.
    private void detachControllerObjects(Transform target, Transform parent)
    {
        foreach (Valve.VR.InteractionSystem.Hand controller in controllers)
        {
            if (controller != null)
            {
                controller.DetachObject(target.gameObject);

                if (parent != null)
                {
                    controller.DetachObject(parent.gameObject);
                }
            }
        }
    }

/*[public method to despawn boxes.]
* 
* Author: Rollin Poe
* Created: October 2018
* Last Edit: October 2018
* 
* Cognitive Science Lab, Simon Fraser University
* Originally Created for: VR_Fall2018_1
* 
* Attached to next button in unity editor. Triggers On Hand Hover Begin()
*/

    public void buttonDespawn()
    {
        GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");

        foreach (GameObject cube in currentObjs)
        {
            if (cube.GetComponent<CustomTag>().getTag(1) != "") { 
                Debug.Log("Destroyed " + cube);
                Destroy(cube);
            }
        }

    }

}
