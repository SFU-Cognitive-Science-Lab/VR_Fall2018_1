using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour {
    public int Seconds;
    public string NextScene;
    private float timeout;

	// Use this for initialization
	void Start ()
    {
        timeout = Seconds;    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0.0)
        {
            SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
        }
    }
}
