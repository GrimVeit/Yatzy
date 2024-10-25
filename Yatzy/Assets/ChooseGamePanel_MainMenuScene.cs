using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGamePanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button gameSoloButton;
    [SerializeField] private Button gameBotButton;
    [SerializeField] private Button gameFriendButton;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backButton.onClick.AddListener(HandlerClickToBackButton);
        gameSoloButton.onClick.AddListener(HandlerClickToGameSoloButton);
        gameBotButton.onClick.AddListener(HandlerClickToGameBotButton);
        gameFriendButton.onClick.AddListener(HandlerClickToGameFriendButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backButton.onClick.RemoveListener(HandlerClickToBackButton);
        gameSoloButton.onClick.RemoveListener(HandlerClickToGameSoloButton);
        gameBotButton.onClick.RemoveListener(HandlerClickToGameBotButton);
        gameFriendButton.onClick.RemoveListener(HandlerClickToGameFriendButton);
    }

    #region Input

    public event Action OnClickBackButton;

    public event Action OnClickToGameSoloButton;
    public event Action OnClickToGameBotButton;
    public event Action OnClickToGameFriendButton;

    private void HandlerClickToBackButton()
    {
        soundProvider.PlayOneShot("Click");
        OnClickBackButton?.Invoke();
    }

    private void HandlerClickToGameSoloButton()
    {
        OnClickToGameSoloButton?.Invoke();
    }

    private void HandlerClickToGameBotButton()
    {
        OnClickToGameBotButton?.Invoke();
    }

    private void HandlerClickToGameFriendButton()
    {
        OnClickToGameFriendButton?.Invoke();
    }

    #endregion
}
