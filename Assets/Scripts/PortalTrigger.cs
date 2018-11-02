using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour {
    
    public string interactableTag;
    public string targetTag;

    public Valve.VR.InteractionSystem.Hand fallbackController;
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;

    private List<Valve.VR.InteractionSystem.Hand> controllers;

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
        Transform otherTransform = other.transform;
        Transform parentTransform = otherTransform.parent;

        CustomTag customTag = otherTransform.GetComponent(typeof(CustomTag)) as CustomTag;

        if (otherTransform.CompareTag(interactableTag) && customTag != null && customTag.hasTag(targetTag))
        {
            detachControllerObjects(otherTransform, parentTransform);

            // Destroy Other Children
            foreach (Transform child in otherTransform)
            {
                Destroy(child.gameObject);
            }

            // Destroy Other
            Destroy(otherTransform.gameObject);

            // Destroy Other Parent
            if (parentTransform != null)
            {
                Destroy(parentTransform.gameObject);
            }
        }
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
}
