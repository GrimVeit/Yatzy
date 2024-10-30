using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombination_FullHouse : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] protected private Button buttonChooseCombination;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteUnselect;

    private YatzyCombinationData yatzyCombinationData;

    public override void Initialize(YatzyCombinationData yatzyCombinationData)
    {
        this.yatzyCombinationData = yatzyCombinationData;

        buttonChooseCombination.onClick.AddListener(HandlerClickToChooseCombinationButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonChooseCombination.onClick.RemoveListener(HandlerClickToChooseCombinationButton);
    }

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

        yatzyCombinationData.SetScore(result);
    }

    public override void Select()
    {
        yatzyCombinationData.SetSelect(true);
        buttonImage.sprite = spriteSelect;
    }

    public override void Unselect()
    {
        yatzyCombinationData.SetSelect(false);
        buttonImage.sprite = spriteUnselect;
    }

    public override void Freeze()
    {
        buttonChooseCombination.enabled = false;
        buttonImage.sprite = spriteSelect;

        yatzyCombinationData.SetFreeze(true);
    }

    #region Input

    private void HandlerClickToChooseCombinationButton()
    {
        OnChooseCombination?.Invoke(yatzyCombinationData);
    }

    #endregion
}
