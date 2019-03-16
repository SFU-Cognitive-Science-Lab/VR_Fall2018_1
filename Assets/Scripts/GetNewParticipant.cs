using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetNewParticipant : MonoBehaviour {
    public Button button;
    public InputField participantID;
    public InputField cubeset;
    public InputField arrangement;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    // Use this for initialization
    void Start () {
        button.onClick.AddListener(NewParticipantListener);
    }
	
	// Update is called once per frame
	void NewParticipantListener () {
        participantID.text = ps.GetParticipantAsString();
        ps.ConditionFromParticipant();
        ParticipantStatus.Condition cond = ps.GetCondition();
        cubeset.text = cond.cubeset.ToString();
        arrangement.text = cond.catmap.ToString();
    }
}
