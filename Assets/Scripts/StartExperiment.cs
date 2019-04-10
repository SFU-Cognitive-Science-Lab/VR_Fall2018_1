using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartExperiment : MonoBehaviour {
    public string NextScene;
    public Button apply;
    private ParticipantStatus ps;

    // Use this for initialization
    // for the ui to work properly the canvas must use Render Mode "Screen Space - Overlay"
    // this is causing a warning to show up in unity's editor but this has no effect as 
    // we don't want the participant to see the UI
    void Start()
    {
        ps = ParticipantStatus.GetInstance();
        apply.onClick.AddListener(StartExperimentListener);
    }

    // in order for this to work we need an EventSystem component 
    void StartExperimentListener()
    {
        if (ps.GetParticipant() > 0)
        {
            Debug.Log("Starting experiment: condition " + ps.GetCondition() + " for " + ps.GetParticipant());
            if (NextScene != null && NextScene != "")
            {
                SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
            } 
        }
    }

}
