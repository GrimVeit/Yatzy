using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class YatzyCombination_LargeStraight : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;

    public override void CalculateScore(int[] diceValues)
    {
        int result = 0;

        var uniqueValues = diceValues.Distinct().ToArray();

        if (uniqueValues.Length > 5 &&
            uniqueValues.Contains(1) &&
            uniqueValues.Contains(2) &&
            uniqueValues.Contains(3) &&
            uniqueValues.Contains(4) &&
            uniqueValues.Contains(5)
            ||
            uniqueValues.Contains(2)
            && uniqueValues.Contains(3)
            && uniqueValues.Contains(4)
            && uniqueValues.Contains(5)
            && uniqueValues.Contains(6))
        {
            result = 40;
        }

        textScore.text = result.ToString();
    }

    public override void Select()
    {

    }

    public override void Unselect()
    {

    }
}
