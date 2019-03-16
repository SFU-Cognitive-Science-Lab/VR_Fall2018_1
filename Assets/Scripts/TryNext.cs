using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryNext : MonoBehaviour {
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    public GameObject [] TestButtons;
    public Material material;

    public int trials;
    private ParticipantStatus ps;

    void Start()
    {
        ps = ParticipantStatus.GetInstance();
        ps.ResetCubes();
    }

    private void OnTriggerStay(Collider other)
    {
        if (leftController.controller.GetHairTriggerUp() || rightController.controller.GetHairTriggerUp())
        {
            //reset selection state
            TestState.SetSelectedFalse();

            //reset all buttons back to grey (no choice)
            for(int i = 0; i < 4; i++)
            {
                TestButtons[i].GetComponent<Renderer>().material = material;
            }

            if (trials > 0)
            {
                //Despawn current box
                bool any = true;
                this.GetComponent<DespawnObject>().buttonDespawn(any);

                ps.GetNextStimulus();
                trials--;
                Debug.Log("test trials left " + trials);
                //Spawn new box
                bool always = true;
                this.GetComponent<SpawnRandomBox>().spawn(always);
            }
            else
            {
                this.GetComponent<DespawnObject>().buttonDespawn();
                Debug.Log("showing instructions");
                ParticipantStatus.GetInstance().ResetCubes();
                SceneManager.LoadScene("ParticipantInstructions", LoadSceneMode.Single);
            }
        }
    }
}
