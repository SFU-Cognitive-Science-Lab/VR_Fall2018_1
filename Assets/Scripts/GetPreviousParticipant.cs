using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// DO NOT USE - entering everything manually now
public class GetPreviousParticipant : MonoBehaviour {
    public Button button;
    public InputField participantID;
    public InputField cubeset;
    public InputField arrangement;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(PreviousParticipantListener);
    }

	void PreviousParticipantListener ()
    {
        if (ps.GetParticipant() > 0)
        {
            participantID.text = ps.GetParticipantAsString();
            ParticipantStatus.Condition cond = ps.GetCondition();
            cubeset.text = cond.cubeset.ToString();
            arrangement.text = cond.catmap.ToString();
        }
    }
}
