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
    private BankPresenter bankPresenter;

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

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        ActivateEvents();

        sceneRoot.Activate();
        sceneRoot.OpenMainPanel();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();
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
        bankPresenter?.Dispose();
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
