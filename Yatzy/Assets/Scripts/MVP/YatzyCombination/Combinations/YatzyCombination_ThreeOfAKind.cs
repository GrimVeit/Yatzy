using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class YatzyCombination_ThreeOfAKind : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;

    public override void CalculateScore(int[] diceValues)
    {
        int result = 0;

        var groupValues = diceValues.GroupBy(d => d);

        foreach (var group in groupValues)
        {
            if(group.Count() >= 3)
            {
                result = group.Key * 3;
            }
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
