using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBox : MonoBehaviour {

    public List<Transform> boxPrefabs;
    public Transform spawnPoint;

    // Use Start() for initialization.
    void Start()
    {	
    }

    // Update() is called once per frame.
    void Update ()
    {
    }

    // OnTriggerEnter() event listener.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            if (boxPrefabs != null)
            {
                // Spawn Random Box
                Transform randomBox = Instantiate(boxPrefabs[Random.Range(0, boxPrefabs.Count)]);
                randomBox.position = spawnPoint.position;
            }
        }
    }

/*[public method to spawn boxes.]
 * 
 * Author: Rollin Poe
 * Created: October 2018
 * Last Edit: October 2018
 * 
 * Cognitive Science Lab, Simon Fraser University
 * Originally Created for: VR_Fall2018_1
 * 
 * Attached to next button in unity editor. Triggers On Hand Hover End()
 */

    public void spawn()
    {
        int objects = 0;
        GameObject[] currentObjs = GameObject.FindGameObjectsWithTag("Interactable Object");

        foreach (GameObject cube in currentObjs)
        {
            if (cube.GetComponent<CustomTag>().getTag(1) == "")
            {
                objects += 1;
            }
        }

        if (boxPrefabs != null && objects == 0)
        {
            // Spawn Random Box
            Transform randomBox = Instantiate(boxPrefabs[Random.Range(0, boxPrefabs.Count)]);
            randomBox.position = spawnPoint.position;
            randomBox.rotation = spawnPoint.rotation;
        }

    }

}
