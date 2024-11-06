using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_MainMenuScene mainPanel;
    [SerializeField] private RegistrationPanel_MainMenuScene registrationPanel;
    [SerializeField] private ChooseImagePanel_MainMenuScene chooseImagePanel;
    [SerializeField] private ChooseImagePanel_MainMenuScene chooseChangeImagePanel;
    [SerializeField] private RegistrationDonePanel_MainMenuScene registrationDonePanel;
    [SerializeField] private LeadersPanel_MainMenuScene leadersPanel;
    [SerializeField] private ChooseGamePanel_MainMenuScene chooseGamePanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        registrationPanel.Initialize();
        chooseImagePanel.Initialize();
        chooseChangeImagePanel.Initialize();
        registrationDonePanel.Initialize();
        leadersPanel.Initialize();
        chooseGamePanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.GoToChooseGamePanel_Action += HandleGoToChooseGamePanelFromMainPanel;
        mainPanel.GoToLeadersPanel_Action += HandleGoToLeadersPanelFromMainPanel;

        registrationDonePanel.OnClickToBackButton += HandleGoToMainPanelFromRegistrationDonePanel;
        registrationPanel.OnClickToChooseImageButton += HandleGoToChooseImagePanelFromRegistrationPanel;
        chooseImagePanel.OnClickToBackInRegistrationButton += HandleGoToRegistrationPanelFromChooseImagePanel;
        chooseChangeImagePanel.OnClickToBackInRegistrationButton += HandleGoToLeadersPanelFromChooseChangeImagePanel;
        leadersPanel.OnClickBackButton += HandleGoToMainPanelFromLeadersPanel;
        leadersPanel.OnClickToChangeAvatarButton += HandleGoToChooseChangeImagePanelFromLeadersPanel;
        chooseGamePanel.OnClickBackButton += HandleGoToMainPanelFromChooseGamePanel;
    }

    public void Deactivate()
    {
        mainPanel.GoToChooseGamePanel_Action -= HandleGoToChooseGamePanelFromMainPanel;
        mainPanel.GoToLeadersPanel_Action -= HandleGoToLeadersPanelFromMainPanel;

        registrationDonePanel.OnClickToBackButton -= HandleGoToMainPanelFromRegistrationDonePanel;
        registrationPanel.OnClickToChooseImageButton -= HandleGoToChooseImagePanelFromRegistrationPanel;
        chooseImagePanel.OnClickToBackInRegistrationButton -= HandleGoToRegistrationPanelFromChooseImagePanel;
        chooseChangeImagePanel.OnClickToBackInRegistrationButton -= HandleGoToLeadersPanelFromChooseChangeImagePanel;
        leadersPanel.OnClickBackButton -= HandleGoToMainPanelFromLeadersPanel;
        leadersPanel.OnClickToChangeAvatarButton -= HandleGoToChooseChangeImagePanelFromLeadersPanel;
        chooseGamePanel.OnClickBackButton -= HandleGoToMainPanelFromChooseGamePanel;

        currentPanel.DeactivatePanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        registrationPanel.Dispose();
        chooseImagePanel.Dispose();
        chooseChangeImagePanel.Dispose();
        registrationDonePanel.Dispose();
        leadersPanel.Dispose();
        chooseGamePanel.Dispose();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenChooseGamePanel()
    {
        OpenPanel(chooseGamePanel);
    }

    public void OpenLeadersPanel()
    {
        OpenPanel(leadersPanel);
    }

    public void OpenRegistrationPanel()
    {
        OpenPanel(registrationPanel);
    }

    public void OpenChooseImagePanel()
    {
        OpenPanel(chooseImagePanel);
    }

    public void OpenChooseChangeImagePanel()
    {
        OpenPanel(chooseChangeImagePanel);
    }

    public void OpenRegistrationDonePanel()
    {
        OpenPanel(registrationDonePanel);
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

    public event Action OnGoToChooseGamePanelFromMainPanel;
    public event Action OnGoToLeadersPanelFromMainPanel;
    public event Action OnGoToMainPanelFromRegistrationDonePanel;
    public event Action OnGoToMainPanelFromChooseGamePanel;
    public event Action OnGoToMainPanelFromLeadersPanel;
    public event Action OnGoToChooseChangeImagePanelFromLeadersPanel;
    public event Action OnGoToRegistrationPanelFromChooseImagePanel;
    public event Action OnGoToLeadersPanelFromChooseChangeImagePanel;
    public event Action OnGoToChooseImagePanelFromRegistrationPanel;

    private void HandleGoToChooseGamePanelFromMainPanel()
    {
        OnGoToChooseGamePanelFromMainPanel?.Invoke();
    }

    private void HandleGoToLeadersPanelFromMainPanel()
    {
        OnGoToLeadersPanelFromMainPanel?.Invoke();
    }

    private void HandleGoToMainPanelFromRegistrationDonePanel()
    {
        OnGoToMainPanelFromRegistrationDonePanel?.Invoke();
    }

    private void HandleGoToMainPanelFromLeadersPanel()
    {
        OnGoToMainPanelFromLeadersPanel?.Invoke();
    }

    private void HandleGoToChooseChangeImagePanelFromLeadersPanel()
    {
        OnGoToChooseChangeImagePanelFromLeadersPanel?.Invoke();
    }

    private void HandleGoToMainPanelFromChooseGamePanel()
    {
        OnGoToMainPanelFromChooseGamePanel?.Invoke();
    }

    private void HandleGoToLeadersPanelFromChooseChangeImagePanel()
    {
        OnGoToLeadersPanelFromChooseChangeImagePanel?.Invoke();
    }

    private void HandleGoToRegistrationPanelFromChooseImagePanel()
    {
        OnGoToRegistrationPanelFromChooseImagePanel?.Invoke();
    }

    private void HandleGoToChooseImagePanelFromRegistrationPanel()
    {
        OnGoToChooseImagePanelFromRegistrationPanel?.Invoke();
    }


    public event Action OnClickToSoloGame
    {
        add { chooseGamePanel.OnClickToGameSoloButton += value; }
        remove { chooseGamePanel.OnClickToGameSoloButton -= value; }
    }

    public event Action OnClickToBotGame
    {
        add { chooseGamePanel.OnClickToGameBotButton += value; }
        remove { chooseGamePanel.OnClickToGameBotButton -= value; }
    }

    public event Action OnClickToFriendGame
    {
        add { chooseGamePanel.OnClickToGameFriendButton += value; }
        remove { chooseGamePanel.OnClickToGameFriendButton -= value; }
    }

    #endregion
}
