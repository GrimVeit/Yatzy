using System;
using UnityEngine;

public class UIGameSoloRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_GameSoloScene mainPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
    }

    public void Activate()
    {
        OpenMainPanel();
    }

    public void Deactivate()
    {

    }

    public void Dispose()
    {
        mainPanel.Dispose();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
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

    public event Action OnClickToBackButton
    {
        add { mainPanel.OnClickToBackButton += value; }
        remove { mainPanel.OnClickToBackButton -= value; }
    }

    #endregion
}
