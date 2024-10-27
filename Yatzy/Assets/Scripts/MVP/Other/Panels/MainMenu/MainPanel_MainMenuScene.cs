using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button play_Button;
    [SerializeField] private Button leaders_Button;

    public event Action GoToChooseGamePanel_Action;
    public event Action GoToLeadersPanel_Action;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        play_Button.onClick.AddListener(HandleGoToChooseGamePanel);
        leaders_Button.onClick.AddListener(HandleGoToLeadersPanel);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        play_Button.onClick.RemoveListener(HandleGoToChooseGamePanel);
        leaders_Button.onClick.RemoveListener(HandleGoToLeadersPanel);
    }

    private void HandleGoToChooseGamePanel()
    {
        GoToChooseGamePanel_Action?.Invoke();
    }

    private void HandleGoToLeadersPanel()
    {
        GoToLeadersPanel_Action?.Invoke();
    }
}
