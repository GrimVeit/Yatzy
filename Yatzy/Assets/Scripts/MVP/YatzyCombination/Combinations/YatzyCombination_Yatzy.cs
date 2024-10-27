using System.Linq;
using TMPro;
using UnityEngine;

public class YatzyCombination_Yatzy : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;

    public override void CalculateScore(int[] diceValues)
    {
        int result = 0;

        if (diceValues.Distinct().Count() == 1)
        {
            result = 50;
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
