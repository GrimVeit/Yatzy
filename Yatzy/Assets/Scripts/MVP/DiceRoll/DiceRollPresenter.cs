using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollPresenter
{
    private DiceRollModel diceRollModel;
    private DiceRollView diceRollView;

    public DiceRollPresenter(DiceRollModel diceRollModel, DiceRollView diceRollView)
    {
        this.diceRollModel = diceRollModel;
        this.diceRollView = diceRollView;
    }

    public void Initialize()
    {
        ActivateEvents();

        diceRollModel.Initialize();
        diceRollView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        diceRollModel.Dispose();
        diceRollView.Dispose();
    }

    private void ActivateEvents()
    {
        diceRollView.OnClickToRollButton += diceRollModel.Roll;
        diceRollView.OnStoppedDice_Action += diceRollModel.OnStopDice;

        diceRollModel.OnRoll += diceRollView.Roll;
    }

    private void DeactivateEvents()
    {
        diceRollView.OnClickToRollButton -= diceRollModel.Roll;
        diceRollView.OnStoppedDice_Action -= diceRollModel.OnStopDice;

        diceRollModel.OnRoll -= diceRollView.Roll;
    }

    #region Input

    public event Action<int[]> OnGetAllDiceValues
    {
        add { diceRollModel.OnGetAllDiceValues += value; }
        remove { diceRollModel.OnGetAllDiceValues -= value; }
    }

    #endregion
}
