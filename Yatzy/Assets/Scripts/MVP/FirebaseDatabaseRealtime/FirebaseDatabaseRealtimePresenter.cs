using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseRealtimePresenter
{
    private FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel;
    private FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView;

    public FirebaseDatabaseRealtimePresenter(FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel, FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView)
    {
        this.firebaseDatabaseRealtimeModel = firebaseDatabaseRealtimeModel;
        this.firebaseDatabaseRealtimeView = firebaseDatabaseRealtimeView;
    }

    public void Initialize()
    {
        ActivateEvents();

        firebaseDatabaseRealtimeModel.Initialize();
        firebaseDatabaseRealtimeView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        firebaseDatabaseRealtimeView.Dispose();
    }

    private void ActivateEvents()
    {
        firebaseDatabaseRealtimeView.OnChangeAvatar += firebaseDatabaseRealtimeModel.SaveChangesToServer;

        firebaseDatabaseRealtimeModel.OnGetUsersRecords += firebaseDatabaseRealtimeView.DisplayUsersRecords;
        firebaseDatabaseRealtimeModel.OnGetNickname += firebaseDatabaseRealtimeView.TestDebugNickname;
        firebaseDatabaseRealtimeModel.OnGetAvatar += firebaseDatabaseRealtimeView.TestDebugAvatar;
    }
    private void DeactivateEvents()
    {
        firebaseDatabaseRealtimeView.OnChangeAvatar -= firebaseDatabaseRealtimeModel.SaveChangesToServer;

        firebaseDatabaseRealtimeModel.OnGetUsersRecords -= firebaseDatabaseRealtimeView.DisplayUsersRecords;
    }

    #region Input

    public void CreateEmptyDataToServer()
    {
        firebaseDatabaseRealtimeModel.CreateNewAccountInServer();
    }

    public void DisplayUsersRecords()
    {
        firebaseDatabaseRealtimeModel.DisplayUsersRecords();
    }

    public void SetNickname(string nickname)
    {
        firebaseDatabaseRealtimeModel.SetNickname(nickname);
    }

    public void SetAvatar(int avatar)
    {
        firebaseDatabaseRealtimeModel.SetAvatar(avatar);
    }

    #endregion
}
