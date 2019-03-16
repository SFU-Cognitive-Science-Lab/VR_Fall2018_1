using UnityEngine;

public class GetChoice : MonoBehaviour {
    public Valve.VR.InteractionSystem.Hand leftController;
    public Valve.VR.InteractionSystem.Hand rightController;
    public string choice;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    void OnTriggerStay(Collider other) {
        if (leftController.controller.GetHairTriggerUp() || rightController.controller.GetHairTriggerUp())
        {
            if (ps.SetChoice(choice))
            {
                bool saveme = true;
                ps.GetDataFarmer().Save(new DFAnswerSelection(), saveme);
                Debug.Log(string.Format("Answer selected: {0}", ps.GetLastChoice()));
            }
        }
	}

}
