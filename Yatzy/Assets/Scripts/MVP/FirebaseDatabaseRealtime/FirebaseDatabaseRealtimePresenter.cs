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
        firebaseDatabaseRealtimeView.OnChooseImage += firebaseDatabaseRealtimeModel.ChooseImage;

        firebaseDatabaseRealtimeModel.OnGetUsersRecords += firebaseDatabaseRealtimeView.DisplayUsersRecords;
        firebaseDatabaseRealtimeModel.OnSelectIndex += firebaseDatabaseRealtimeView.Select;
        firebaseDatabaseRealtimeModel.OnDeselectIndex += firebaseDatabaseRealtimeView.Deselect;
    }

    private void DeactivateEvents()
    {
        firebaseDatabaseRealtimeView.OnChooseImage -= firebaseDatabaseRealtimeModel.ChooseImage;

        firebaseDatabaseRealtimeModel.OnGetUsersRecords -= firebaseDatabaseRealtimeView.DisplayUsersRecords;
        firebaseDatabaseRealtimeModel.OnSelectIndex -= firebaseDatabaseRealtimeView.Select;
        firebaseDatabaseRealtimeModel.OnDeselectIndex -= firebaseDatabaseRealtimeView.Deselect;
    }

    public void CreateEmptyDataToServer()
    {
        firebaseDatabaseRealtimeModel.CreateEmptyDataToServer();
    }

    public void DisplayUsersRecords()
    {
        firebaseDatabaseRealtimeModel.DisplayUsersRecords();
    }
}
