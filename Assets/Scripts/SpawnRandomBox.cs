using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*[public method to spawn boxes.]
 * 
 * Initial Author: Rollin Poe
 * Massacred by: Cal Woodruff
 * Created: October 2018
 * Last Edit: Dec 2018
 * 
 * Cognitive Science Lab, Simon Fraser University
 * Originally Created for: VR_Fall2018_1
 * 
 * Called by Next Button's ChoiceBehaviour.ResetButtons()
 * 
 * Cal: The code to decide on what cube to show next is currently
 * handled by ParticipantStatus. We can reuse the same box as we
 * no longer need the box to know the category. The upshot is that
 * the randomness is not done here so the name is a bit misleading.
 */


public class SpawnRandomBox : MonoBehaviour {

    public Transform boxPrefab;
    public Transform spawnPoint;

    // OnTriggerEnter() event listener.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            if (boxPrefab != null && spawnPoint != null)
            {
                boxPrefab.position = spawnPoint.position;
            }
        }
    }

    public void spawn(bool always = false)
    {
        // this determines if anything is already on display
        // without it you get a "ball" of superimposed boxes which is quite mesmerizing ...
        int objects = 0;

        if (always)
        {
            Debug.Log("always spawning");
        }
        else
        {
            GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");
            foreach (GameObject cube in currentObjs)
            {
                if (cube.GetComponent<CustomTag>().getTag(1) == "")
                {
                    objects += 1;
                }
            }
        }
        if (boxPrefab != null && (objects == 0 || always))
        {
            Transform randomBox = Instantiate(boxPrefab);
            randomBox.position = spawnPoint.position;
            randomBox.rotation = spawnPoint.rotation * Quaternion.Euler(60, 0, 45);
            randomBox.RotateAround(randomBox.position, randomBox.right, 0);
        }

    }
}
