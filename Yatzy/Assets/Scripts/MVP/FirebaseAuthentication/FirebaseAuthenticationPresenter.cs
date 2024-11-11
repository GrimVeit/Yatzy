//using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAuthenticationPresenter
{
    private FirebaseAuthenticationModel firebaseAuthenticationModel;
    private FirebaseAuthenticationView firebaseAuthenticationView;

    public FirebaseAuthenticationPresenter(FirebaseAuthenticationModel firebaseAuthenticationModel, FirebaseAuthenticationView firebaseAuthenticationView)
    {
        this.firebaseAuthenticationModel = firebaseAuthenticationModel;
        this.firebaseAuthenticationView = firebaseAuthenticationView;
    }

    public void Initialize()
    {
        ActivateEvents();

        firebaseAuthenticationModel.Initialize();
        firebaseAuthenticationView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        firebaseAuthenticationView.Dispose();
    }

    private void ActivateEvents()
    {
        firebaseAuthenticationView.OnClickSignUpButton += firebaseAuthenticationModel.SignUp;

        firebaseAuthenticationModel.OnActivate += firebaseAuthenticationView.ActivateRegistrationButton;
        firebaseAuthenticationModel.OnDeactivate += firebaseAuthenticationView.DeactivateRegistrationButton;
        firebaseAuthenticationModel.OnSignUpMessage_Action += firebaseAuthenticationView.SetDescription;
    }

    private void DeactivateEvents()
    {
        firebaseAuthenticationView.OnClickSignUpButton -= firebaseAuthenticationModel.SignUp;

        firebaseAuthenticationModel.OnActivate -= firebaseAuthenticationView.ActivateRegistrationButton;
        firebaseAuthenticationModel.OnDeactivate -= firebaseAuthenticationView.DeactivateRegistrationButton;
        firebaseAuthenticationModel.OnSignUpMessage_Action -= firebaseAuthenticationView.SetDescription;
    }

    #region Input

    public void Activate()
    {
        firebaseAuthenticationModel.Activate();
    }

    public void Deactivate()
    {
        firebaseAuthenticationModel.Deactivate();
    }
    public bool CheckAuthenticated()
    {
        return firebaseAuthenticationModel.CheckUserAuthentication();
    }

    public void DeleteAccount()
    {
        firebaseAuthenticationModel.DeleteAccount();
    }

    public void SignOut()
    {
        firebaseAuthenticationModel.SignOut();
    }

    public void SetNickname(string nickname)
    {
        firebaseAuthenticationModel.SetNickname(nickname);
    }

    public event Action<string> OnChangeCurrentUser
    {
        add { firebaseAuthenticationModel.OnChangeUser += value; }
        remove { firebaseAuthenticationModel.OnChangeUser -= value; }
    }

    public event Action OnSignIn
    {
        add { firebaseAuthenticationModel.OnSignIn_Action += value; }
        remove { firebaseAuthenticationModel.OnSignIn_Action -= value; }
    }

    public event Action OnSignUp
    {
        add { firebaseAuthenticationModel.OnSignUp_Action += value; }
        remove { firebaseAuthenticationModel.OnSignUp_Action -= value; }
    }

    public event Action OnSignOut
    {
        add { firebaseAuthenticationModel.OnSignOut_Action += value; }
        remove { firebaseAuthenticationModel.OnSignOut_Action -= value; }
    }

    public event Action OnDeleteAccoun
    {
        add { firebaseAuthenticationModel.OnDeleteAccount_Action += value; }
        remove { firebaseAuthenticationModel.OnDeleteAccount_Action -= value; }
    }

    #endregion
}
