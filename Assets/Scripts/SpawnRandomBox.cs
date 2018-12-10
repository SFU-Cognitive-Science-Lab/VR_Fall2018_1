using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnRandomBox : MonoBehaviour {

    public List<Transform> boxPrefabs;
    public Transform spawnPoint;
    private int cubeCount;
    private int[] order = { 0, 1, 2, 3, 4, 5, 6, 7 };
    private static System.Random _random = new System.Random();

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
                Transform randomBox = Instantiate(boxPrefabs[UnityEngine.Random.Range(0, boxPrefabs.Count)]);
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
        if(cubeCount == 0 & this.GetComponent<CustomTag > ().getTag(0) == "NEXT")
        {
            Shuffle(order);
        }

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
            //Transform randomBox = Instantiate(boxPrefabs[Random.Range(0, boxPrefabs.Count)]); //boxPrefabs[0]); // 
            Transform randomBox = Instantiate(boxPrefabs[cubeCount]);
            ParticipantStatus.GetInstance().SetCube(randomBox);
            randomBox.position = spawnPoint.position;
            randomBox.rotation = spawnPoint.rotation * Quaternion.Euler(60, 0, 45);

            /*
             * this, given the same cube, should cycle through each side being on top
             * if you want to cycle through them very quickly hold the buttons down on NEXT and one of the choices
            AxisOrientation[] axes = {
                new AxisOrientation(randomBox.up, 0),
                new AxisOrientation(randomBox.up, 90),
                new AxisOrientation(randomBox.forward, 90),
                new AxisOrientation(randomBox.right, 90),
                new AxisOrientation(randomBox.up, 180),
                new AxisOrientation(randomBox.up, 270),
                new AxisOrientation(randomBox.forward, 270),
                new AxisOrientation(randomBox.right, 270),
            };
            randomBox.RotateAround(randomBox.position, axes[cubeCount].Axis, axes[cubeCount].Orientation);
            */

            randomBox.RotateAround(randomBox.position, randomBox.right, 0);
            //randomBox.RotateAround(randomBox.position, randomBox.right, 90);
            //randomBox.RotateAround(randomBox.position, randomBox.up, 90);

            cubeCount++;
            if(cubeCount > 7)
            {
                cubeCount = 0;
            }
        }

    }

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + _random.Next(n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    class AxisOrientation
    {
        public Vector3 Axis { get; set; }
        public int Orientation { get; set;  }
        public AxisOrientation(Vector3 a, int o)
        {
            Axis = a;
            Orientation = o;
        }
    }

}
