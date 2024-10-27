using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class YatzyCombination_FullHouse : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;

    public override void CalculateScore(int[] diceValues)
    {
        int result = 0;

        var groupValues = diceValues.GroupBy(d => d).Select(g => g.Count()).ToArray();

        bool hasTree = groupValues.Contains(3);
        bool hasTwo = groupValues.Contains(2);

        if(hasTree && hasTwo)
        {
            result = 25;
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
