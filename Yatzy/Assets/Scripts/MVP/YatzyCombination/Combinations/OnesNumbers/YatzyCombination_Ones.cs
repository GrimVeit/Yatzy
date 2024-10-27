using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombination_Ones : YatzyCombination
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] protected private Button buttonChooseCombination;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite spriteSelect;
    [SerializeField] private Sprite spriteUnselect;

    public override void Initialize()
    {
        base.Initialize();

        buttonChooseCombination.onClick.AddListener(HandlerClickToChooseCombinationButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonChooseCombination.onClick.RemoveListener(HandlerClickToChooseCombinationButton);
    }

    public override void CalculateScore(int[] diceValues)
    {
        int result = diceValues.Count(d => d == 1);

        textScore.text = result.ToString();
    }

    public override void Select()
    {
        buttonImage.sprite = spriteSelect;
    }

    public override void Unselect()
    {
        buttonImage.sprite = spriteUnselect;
    }

    #region Input

    private void HandlerClickToChooseCombinationButton()
    {
        OnChooseCombination?.Invoke(this);
    }

    #endregion
}
