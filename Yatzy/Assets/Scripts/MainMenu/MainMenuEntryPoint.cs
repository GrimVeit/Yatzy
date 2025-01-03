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
    private AvatarPresenter avatarPresenter;
    private AvatarPresenter avatarPresenterChanges;
    private NicknamePresenter nicknamePresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        nicknamePresenter = new NicknamePresenter
            (new NicknameModel(PlayerPrefsKeys.NICKNAME, soundPresenter),
            viewContainer.GetView<NicknameView>());

        avatarPresenter = new AvatarPresenter
            (new AvatarModel(PlayerPrefsKeys.IMAGE_INDEX, soundPresenter),
            viewContainer.GetView<AvatarView>("Main"));

        avatarPresenterChanges = new AvatarPresenter
            (new AvatarModel(PlayerPrefsKeys.IMAGE_INDEX, soundPresenter),
            viewContainer.GetView<AvatarView>("Changes"));

        firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter
            (new FirebaseAuthenticationModel(firebaseAuth, soundPresenter, particleEffectPresenter),
            viewContainer.GetView<FirebaseAuthenticationView>());

        firebaseDatabaseRealtimePresenter = new FirebaseDatabaseRealtimePresenter
            (new FirebaseDatabaseRealtimeModel(firebaseAuth, databaseReference, soundPresenter),
            viewContainer.GetView<FirebaseDatabaseRealtimeView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();


        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        nicknamePresenter.Initialize();
        avatarPresenter.Initialize();
        avatarPresenterChanges.Initialize();
        firebaseAuthenticationPresenter.Initialize();
        firebaseDatabaseRealtimePresenter.Initialize();
        sceneRoot.Initialize();

        if (firebaseAuthenticationPresenter.CheckAuthenticated())
        {
            sceneRoot.OpenMainPanel();
        }
        else
        {
            sceneRoot.OpenRegistrationPanel();
        }
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        nicknamePresenter.OnCorrectNickname += firebaseAuthenticationPresenter.Activate;
        nicknamePresenter.OnIncorrectNickname += firebaseAuthenticationPresenter.Deactivate;

        nicknamePresenter.OnGetNickname += firebaseAuthenticationPresenter.SetNickname;
        nicknamePresenter.OnGetNickname += firebaseDatabaseRealtimePresenter.SetNickname;
        avatarPresenter.OnGetAvatar += firebaseDatabaseRealtimePresenter.SetAvatar;
        avatarPresenterChanges.OnGetAvatar += firebaseDatabaseRealtimePresenter.SetAvatar;

        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.DisplayUsersRecords;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        nicknamePresenter.OnCorrectNickname -= firebaseAuthenticationPresenter.Activate;
        nicknamePresenter.OnIncorrectNickname -= firebaseAuthenticationPresenter.Deactivate;

        nicknamePresenter.OnGetNickname -= firebaseAuthenticationPresenter.SetNickname;
        nicknamePresenter.OnGetNickname -= firebaseDatabaseRealtimePresenter.SetNickname;
        avatarPresenter.OnGetAvatar -= firebaseDatabaseRealtimePresenter.SetAvatar;
        avatarPresenterChanges.OnGetAvatar -= firebaseDatabaseRealtimePresenter.SetAvatar;

        firebaseAuthenticationPresenter.OnSignUp -= firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        firebaseAuthenticationPresenter.OnSignUp -= firebaseDatabaseRealtimePresenter.DisplayUsersRecords;
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
        sceneRoot.OnGoToChooseChangeImagePanelFromLeadersPanel += sceneRoot.OpenChooseChangeImagePanel;
        sceneRoot.OnGoToLeadersPanelFromChooseChangeImagePanel += sceneRoot.OpenLeadersPanel;
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
        sceneRoot.OnGoToChooseChangeImagePanelFromLeadersPanel -= sceneRoot.OpenChooseChangeImagePanel;
        sceneRoot.OnGoToLeadersPanelFromChooseChangeImagePanel -= sceneRoot.OpenLeadersPanel;
        firebaseAuthenticationPresenter.OnSignUp -= sceneRoot.OpenRegistrationDonePanel;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        nicknamePresenter?.Dispose();
        avatarPresenter?.Dispose();
        avatarPresenterChanges?.Dispose();
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
        soundPresenter.PlayOneShot("ClickEnter");
        GoToSoloGame_Action?.Invoke();
    }

    private void HandleGoToBotGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        GoToBotGame_Action?.Invoke();
    }

    private void HandlerGoToFriendGame()
    {
        Deactivate();
        soundPresenter.PlayOneShot("ClickEnter");
        GoToFriendGame_Action?.Invoke();
    }

    #endregion
}
