using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameFriendRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_GameSoloScene mainPanel;
    [SerializeField] private FinishPanel_GameSoloScene finishWinPanel;
    [SerializeField] private FinishPanel_GameSoloScene finishLosePanel;

    [Header("My panels")]
    [SerializeField] private GameButtonsPanel_BotGamePanel gameButtonsPanel_Me;
    [SerializeField] private RollPanel_GameSoloScene rollPanel_Me;
    [SerializeField] private RollPlayPanel_GameSoloScene rollPlayPanel_Me;

    [Header("Bot panels")]
    [SerializeField] private GameButtonsPanel_BotGamePanel gameButtonsPanel_Bot;
    [SerializeField] private RollPanel_GameSoloScene rollPanel_Bot;
    [SerializeField] private RollPlayPanel_GameSoloScene rollPlayPanel_Bot;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        finishWinPanel.Initialize();
        finishLosePanel.Initialize();

        gameButtonsPanel_Me.Initialize();
        rollPanel_Me.Initialize();
        rollPlayPanel_Me.Initialize();

        gameButtonsPanel_Bot.Initialize();
        rollPanel_Bot.Initialize();
        rollPlayPanel_Bot.Initialize();
    }

    public void Activate()
    {
        OpenMainPanel();
        OpenGamePanel_Me();
        OpenRollPanel_Me();
    }

    public void Deactivate()
    {
        currentPanel.DeactivatePanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        finishWinPanel.Dispose();
        finishLosePanel.Dispose();

        gameButtonsPanel_Me.Dispose();
        rollPanel_Me.Dispose();
        rollPlayPanel_Me.Dispose();

        gameButtonsPanel_Bot.Dispose();
        rollPanel_Bot.Dispose();
        rollPlayPanel_Bot.Dispose();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenWinFinishPanel()
    {
        OpenPanel(finishWinPanel);
    }

    public void OpenLoseFinishPanel()
    {
        OpenPanel(finishLosePanel);
    }


    public void OpenGamePanel_Me()
    {
        if (gameButtonsPanel_Bot.IsActivePanel)
        {
            CloseOtherPanel(gameButtonsPanel_Bot);
        }

        OpenOtherPanel(gameButtonsPanel_Me);
    }

    public void OpenGamePanel_Bot()
    {
        if (gameButtonsPanel_Me.IsActivePanel)
        {
            CloseOtherPanel(gameButtonsPanel_Me);
        }

        OpenOtherPanel(gameButtonsPanel_Bot);
    }


    public void OpenRollPanel_Me()
    {
        if (rollPlayPanel_Me.IsActivePanel)
        {
            CloseOtherPanel(rollPlayPanel_Me);
        }

        OpenOtherPanel(rollPanel_Me);
    }

    public void OpenPlayRollPanel_Me()
    {
        if (rollPanel_Me.IsActivePanel)
        {
            CloseOtherPanel(rollPanel_Me);
        }

        OpenOtherPanel(rollPlayPanel_Me);
    }

    public void OpenRollPanel_Bot()
    {
        if (rollPlayPanel_Bot.IsActivePanel)
        {
            CloseOtherPanel(rollPlayPanel_Bot);
        }

        OpenOtherPanel(rollPanel_Bot);
    }

    public void OpenPlayRollPanel_Bot()
    {
        if (rollPanel_Bot.IsActivePanel)
        {
            CloseOtherPanel(rollPanel_Bot);
        }

        OpenOtherPanel(rollPlayPanel_Bot);
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }


    #region Input Actions

    public event Action OnClickToGoMainMenuFromMainPanel
    {
        add { mainPanel.OnClickToGoMainMenu += value; }
        remove { mainPanel.OnClickToGoMainMenu -= value; }
    }

    public event Action OnClickToGoMainMenuFromWinFinishPanel
    {
        add { finishWinPanel.OnGoToMainMenu += value; }
        remove { finishWinPanel.OnGoToMainMenu -= value; }
    }

    public event Action OnClickToGoSoloGameFromWinFinishPanel
    {
        add { finishWinPanel.OnGoToSoloGame += value; }
        remove { finishWinPanel.OnGoToSoloGame -= value; }
    }

    public event Action OnClickToGoMainMenuFromLoseFinishPanel
    {
        add { finishLosePanel.OnGoToMainMenu += value; }
        remove { finishLosePanel.OnGoToMainMenu -= value; }
    }

    public event Action OnClickToGoSoloGameFromLoseFinishPanel
    {
        add { finishLosePanel.OnGoToSoloGame += value; }
        remove { finishLosePanel.OnGoToSoloGame -= value; }
    }

    #endregion
}
