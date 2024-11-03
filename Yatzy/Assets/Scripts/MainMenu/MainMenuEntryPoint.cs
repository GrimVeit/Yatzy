using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize(); 

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {

            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter
                    (new FirebaseAuthenticationModel(firebaseAuth, soundPresenter),
                    viewContainer.GetView<FirebaseAuthenticationView>());
                firebaseAuthenticationPresenter.Initialize();

                firebaseDatabaseRealtimePresenter = new FirebaseDatabaseRealtimePresenter
                    (new FirebaseDatabaseRealtimeModel(firebaseAuth, databaseReference),
                    viewContainer.GetView<FirebaseDatabaseRealtimeView>());
                firebaseDatabaseRealtimePresenter.Initialize();

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Initialize();

                ActivateEvents();

                sceneRoot.Activate();

                if (firebaseAuthenticationPresenter.CheckAuthenticated())
                {
                    sceneRoot.OpenMainPanel();
                }
                else
                {
                    sceneRoot.OpenRegistrationPanel();
                }

            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.DisplayUsersRecords;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBotGame += HandleGoToBotGame;
        sceneRoot.OnClickToSoloGame += HandleGoToSoloGame;
        sceneRoot.OnClickToFriendGame += HandlerGoToFriendGame;

        sceneRoot.OnGoToChooseGamePanelFromMainPanel += sceneRoot.OpenChooseGamePanel;
        sceneRoot.OnGoToLeadersPanelFromMainPanel += sceneRoot.OpenLeadersPanel;
        sceneRoot.OnGoToMainPanelFromChooseGamePanel += sceneRoot.OpenMainPanel;
        sceneRoot.OnGoToMainPanelFromLeadersPanel += sceneRoot.OpenMainPanel;
        sceneRoot.OnGoToChooseImagePanelFromRegistrationPanel += sceneRoot.OpenChooseImagePanel;
        sceneRoot.OnGoToRegistrationPanelFromChooseImagePanel += sceneRoot.OpenRegistrationPanel;
        sceneRoot.OnGoToMainPanelFromRegistrationDonePanel += sceneRoot.OpenMainPanel;
        firebaseAuthenticationPresenter.OnSignUp += sceneRoot.OpenRegistrationDonePanel;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBotGame -= HandleGoToBotGame;
        sceneRoot.OnClickToSoloGame -= HandleGoToSoloGame;
        sceneRoot.OnClickToFriendGame -= HandlerGoToFriendGame;

        sceneRoot.OnGoToChooseGamePanelFromMainPanel -= sceneRoot.OpenChooseGamePanel;
        sceneRoot.OnGoToLeadersPanelFromMainPanel -= sceneRoot.OpenLeadersPanel;
        sceneRoot.OnGoToMainPanelFromChooseGamePanel -= sceneRoot.OpenMainPanel;
        sceneRoot.OnGoToMainPanelFromLeadersPanel -= sceneRoot.OpenMainPanel;
        sceneRoot.OnGoToChooseImagePanelFromRegistrationPanel -= sceneRoot.OpenChooseImagePanel;
        sceneRoot.OnGoToRegistrationPanelFromChooseImagePanel -= sceneRoot.OpenRegistrationPanel;
        sceneRoot.OnGoToMainPanelFromRegistrationDonePanel -= sceneRoot.OpenMainPanel;
        firebaseAuthenticationPresenter.OnSignUp -= sceneRoot.OpenRegistrationDonePanel;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
    }

    private void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabaseRealtimePresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action GoToSoloGame_Action;
    public event Action GoToBotGame_Action;
    public event Action GoToFriendGame_Action;


    private void HandleGoToSoloGame()
    {
        Deactivate();
        GoToSoloGame_Action?.Invoke();
    }

    private void HandleGoToBotGame()
    {
        Deactivate();
        GoToBotGame_Action?.Invoke();
    }

    private void HandlerGoToFriendGame()
    {
        Deactivate();
        GoToFriendGame_Action?.Invoke();
    }

    #endregion
}
