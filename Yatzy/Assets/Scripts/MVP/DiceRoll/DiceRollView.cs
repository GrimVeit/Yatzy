using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollView : View
{
    public event Action<int, DiceData> OnStoppedDice_Action;

    [SerializeField] private List<Dice> dices = new List<Dice>();
    //[SerializeField] private 
    [SerializeField] private Button buttonRoll;

    public void Initialize()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].OnStopRotated += OnStoppedDice;
            dices[i].Initialize(i);
        }

        buttonRoll.onClick.AddListener(HandlerClickToRollButton);
    }

    public void Dispose()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].OnStopRotated -= OnStoppedDice;
            dices[i].Dispose();
        }

        buttonRoll.onClick.RemoveListener(HandlerClickToRollButton);
    }

    public void Roll()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].Roll();
        }
    }

    #region Input

    public event Action OnClickToRollButton;

    private void HandlerClickToRollButton()
    {
        OnClickToRollButton?.Invoke();
    }

    private void OnStoppedDice(int index, DiceData diceData)
    {
        OnStoppedDice_Action?.Invoke(index, diceData);
    }

    #endregion
}
