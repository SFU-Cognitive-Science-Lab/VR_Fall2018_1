using UnityEngine;
using System;

public class DFAnswerSelection: DataFarmerObject
{
    private string cubeTag;
    private string rightAnswer;
    private string chosenAnswer;
    // cumulative movement of head and hands
    private Vector3 movement;

	public DFAnswerSelection(float timestamp, string cube, string right, string chosen, Vector3 mv):
                base("answer", timestamp)
    {
        this.cubeTag = cube;
        this.rightAnswer = right;
        this.chosenAnswer = chosen;
        this.movement = mv;
	}

    public override string Serialize(long participant)
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\n", 
            tag, participant, timestamp, 
            cubeTag, rightAnswer, chosenAnswer, 
            movement.x, movement.y, movement.z);
    }
}
