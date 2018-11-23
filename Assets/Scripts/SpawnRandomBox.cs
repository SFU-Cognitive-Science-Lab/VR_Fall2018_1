using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBox : MonoBehaviour {

    public List<Transform> boxPrefabs;
    public Transform spawnPoint;
    private int cubeCount;
    private int[] orientations = { 0, 90, 180, 270 };

    // Use Start() for initialization.
    void Start()
    {
        cubeCount = 0;
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
 * Called by Next Button's ChoiceBehaviour.ResetButtons()
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
            Transform randomBox = Instantiate(boxPrefabs[0]); // Random.Range(0, boxPrefabs.Count)]);
            randomBox.position = spawnPoint.position;
            /*
            if(randomBox.GetComponent<CustomTag>().getTag(0) == "C")
            {
                //randomBox.rotation = spawnPoint.rotation * Quaternion.Euler(60, 0, 45);
                //randomBox.rotation = new Quaternion(.5f, -.2f, .3f, .4f);
            }
            else
            {
               // randomBox.rotation = new Quaternion(.6f, -.7f, -.1f, .4f);
            }
            */
            randomBox.rotation = spawnPoint.rotation * Quaternion.Euler(60, 0, 45);
            int orientation = orientations[cubeCount % orientations.Length];
            randomBox.RotateAround(randomBox.position, randomBox.up, orientation);
            cubeCount++;
            //Debug.Log(spawnPoint.rotation * Quaternion.Euler(165,-45,-15));
        }

    }

}
