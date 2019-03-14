using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneChange : MonoBehaviour {
    public string NextScene;
    public Button apply;

	// Use this for initialization
	public void sceneActivate()
    {
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }

    private void Start()
    {
        apply.onClick.AddListener(sceneActivate);
    }
}
