using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class YatzyCombination_Chance : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;

    public override void CalculateScore(int[] diceValues)
    {
        int result = diceValues.Sum();

        textScore.text = result.ToString();
    }

    public override void Select()
    {

    }

    public override void Unselect()
    {

    }
}
