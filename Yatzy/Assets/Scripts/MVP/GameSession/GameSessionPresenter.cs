using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionPresenter
{
    private GameSessionModel gameSessionModel;
    private GameSessionView gameSessionView;

    public GameSessionPresenter(GameSessionModel gameSessionModel, GameSessionView gameSessionView)
    {
        this.gameSessionModel = gameSessionModel;
        this.gameSessionView = gameSessionView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        gameSessionModel.OnChangedToFirstUser += gameSessionView.ActivateFirstUserDisplay;
        gameSessionModel.OnChangedToSecondUser += gameSessionView.ActivateSecondUserDisplay;
    }

    private void DeactivateEvents()
    {
        gameSessionModel.OnChangedToFirstUser -= gameSessionView.ActivateFirstUserDisplay;
        gameSessionModel.OnChangedToSecondUser -= gameSessionView.ActivateSecondUserDisplay;
    }

    #region Input

    public event Action OnChangedToSecondUser
    {
        add { gameSessionModel.OnChangedToSecondUser += value; }
        remove { gameSessionModel.OnChangedToSecondUser -= value; }
    }

    public event Action OnChangedToFirstUser
    {
        add { gameSessionModel.OnChangedToFirstUser += value; }
        remove { gameSessionModel.OnChangedToFirstUser -= value; }
    }

    public void ChangeToFirstUser()
    {
        gameSessionModel.ChangeToFirstUser();
    }

    public void ChangeToSecondUser()
    {
        gameSessionModel.ChangeToSecondUser();
    }

    #endregion
}
