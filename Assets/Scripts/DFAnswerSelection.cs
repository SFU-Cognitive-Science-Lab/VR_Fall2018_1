using UnityEngine;
using System;

public class DFAnswerSelection: DataFarmerObject
{
    private string chosenAnswer;
    private ParticipantStatus ps = ParticipantStatus.GetInstance();

    public DFAnswerSelection() : base("answer")
    {
        this.chosenAnswer = ps.GetLastChoice();
    }

    public DFAnswerSelection(string choice) : base("answer")
    {
        ps.SetChoice(choice);
        this.chosenAnswer = ps.GetLastChoice();
    }

    public override string Serialize()
    {
        return string.Format("{0},{1}\n", base.Serialize(), chosenAnswer);
    }
}
