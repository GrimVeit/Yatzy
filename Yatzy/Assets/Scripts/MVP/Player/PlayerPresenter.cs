using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter
{
    private PlayerModel playerModel;
    private PlayerView playerView;

    public PlayerPresenter(PlayerModel playerModel, PlayerView playerView)
    {
        this.playerModel = playerModel;
        this.playerView = playerView;
    }

    public void Initialize()
    {
        ActivateEvents();

        playerModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        playerModel.Dispose();
    }

    private void ActivateEvents()
    {
        playerModel.OnGetAvatarIndex += playerView.ChooseAvatar;
        playerModel.OnGetNickname += playerView.ChooseNickname;
    }

    private void DeactivateEvents()
    {
        playerModel.OnGetAvatarIndex -= playerView.ChooseAvatar;
        playerModel.OnGetNickname -= playerView.ChooseNickname;
    }
}
