using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollModel
{
    public event Action<int[]> OnGetAllDiceValues;

    public event Action OnRoll;

    private int[] diceValues;
    private int rolledDiceCount;
    private int diceCount;

    public DiceRollModel(int diceCount)
    {
        this.diceCount = diceCount;
        this.diceValues = new int[diceCount];
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void OnStopDice(int index, DiceData diceData)
    {
        rolledDiceCount -= 1;

        diceValues[index] = diceData.Number;

        Debug.Log("Индекс кубика - " + index + ", Значение кубика - " + diceData.Number);

        if(rolledDiceCount == 0)
        {
            Debug.Log("Все кубики остановились");
            OnGetAllDiceValues?.Invoke(diceValues);
        }
    }

    public void Roll()
    {
        rolledDiceCount = diceCount;
        OnRoll?.Invoke();
    }

    public void FreezeDice()
    {

    }

    public void UnfreezeDice()
    {

    }
}
