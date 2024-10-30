using System;

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
        diceRollView.OnClickToRollButton += diceRollModel.StartRoll;
        diceRollView.OnStoppedDice_Action += diceRollModel.StopRoll;
        diceRollView.OnClickToDice += diceRollModel.FreezeToggle;

        diceRollModel.OnStartRoll_Indexes += diceRollView.Roll;
        diceRollModel.OnChangedAttemptsCount += diceRollView.OnChangeAttempts;

        diceRollModel.OnGetFullAttempt += diceRollView.ActivateButton;
        diceRollModel.OnStartRoll += diceRollView.DeactivateButton;
        diceRollModel.OnActivateRoll += diceRollView.ActivateButton;
        diceRollModel.OnDeactivateRoll += diceRollView.DeactivateButton;

        diceRollModel.OnFreeseDice += diceRollView.FreezeDice;
        diceRollModel.OnUnfreeseDice += diceRollView.UnfreeseDice;
    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public event Action<int[]> OnGetAllDiceValues
    {
        add { diceRollModel.OnGetAllDiceValues += value; }
        remove { diceRollModel.OnGetAllDiceValues -= value; }
    }

    public event Action OnGetFullAttempt
    {
        add { diceRollModel.OnGetFullAttempt += value; }
        remove { diceRollModel.OnGetFullAttempt -= value; }
    }

    public event Action OnLoseFirstAttempt
    {
        add { diceRollModel.OnLoseFirstAttempt += value; }
        remove { diceRollModel.OnLoseFirstAttempt -= value; }
    }

    public event Action OnStartRoll
    {
        add { diceRollModel.OnStartRoll += value; }
        remove { diceRollModel.OnStartRoll -= value; }
    }

    public event Action OnStopRoll
    {
        add { diceRollModel.OnStopRoll += value; }
        remove { diceRollModel.OnStopRoll -= value; }
    }

    public void Reload()
    {
        diceRollModel.Reload();
    }

    public void ActivateFreezeToggle()
    {
        diceRollModel.ActivateFreezeToggle();
    }

    public void DeactivateFreezeToggle()
    {
        diceRollModel.DeactivateFreezeToggle();
    }

    public void StartRoll()
    {
        diceRollModel.StartRoll();
    }

    #endregion
}
