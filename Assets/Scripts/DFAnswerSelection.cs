using UnityEngine;
using System;

public class DFAnswerSelection: DataFarmerObject
{
    private string chosenAnswer;

	public DFAnswerSelection():
                base("answer")
    {
        this.chosenAnswer = DataFarmer.GetInstance().GetChoice();
	}

    public override string Serialize()
    {
        return string.Format("{0},{1}\n", base.Serialize(), chosenAnswer);
    }
}
