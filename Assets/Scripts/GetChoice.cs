using UnityEngine;

public class GetChoice : MonoBehaviour {
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    public string choice;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    void OnTriggerStay(Collider other) {
        if (leftController.controller.GetHairTriggerDown() || rightController.controller.GetHairTriggerDown())
        {
            if (ps.SetChoice(choice))
            {
                ps.GetDataFarmer().Save(new DFAnswerSelection());
                Debug.Log(string.Format("Answer selected: {0}", ps.GetLastChoice()));
            }
        }
	}

}
