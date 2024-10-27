using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoloSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameSoloRoot menuRootPrefab;

    private UIGameSoloRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private DiceRollPresenter diceRollPresenter;
    private YatzyCombinationPresenter yatzyCombinationPresenter;

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

        diceRollPresenter = new DiceRollPresenter
            (new DiceRollModel(5), 
            viewContainer.GetView<DiceRollView>());
        diceRollPresenter.Initialize();

        yatzyCombinationPresenter = new YatzyCombinationPresenter
            (new YatzyCombinationModel(), 
            viewContainer.GetView<YatzyCombinationView>());
        yatzyCombinationPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        ActivateEvents();

        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        diceRollPresenter.OnGetAllDiceValues += yatzyCombinationPresenter.SetNumbersCombination;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        diceRollPresenter.OnGetAllDiceValues -= yatzyCombinationPresenter.SetNumbersCombination;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBackButton += HandleGoToMainMenu;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBackButton -= HandleGoToMainMenu;
    }

    private void Dispose()
    {
        DeactivateEvents();
        sceneRoot.Deactivate();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();

        diceRollPresenter?.Dispose();
        yatzyCombinationPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action GoToMainMenu_Action;


    private void HandleGoToMainMenu()
    {
        GoToMainMenu_Action?.Invoke();
    }

    #endregion
}
