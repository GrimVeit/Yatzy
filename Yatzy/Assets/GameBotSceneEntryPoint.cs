using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBotSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameBotRoot menuRootPrefab;

    private UIGameBotRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private DiceRollPresenter diceRollPresenter_Me;
    private YatzyCombinationPresenter yatzyCombinationPresenter_Me;
    private ScorePresenter scorePresenter_Me;

    private DiceRollPresenter diceRollPresenter_Bot;
    private YatzyCombinationPresenter yatzyCombinationPresenter_Bot;
    private ScorePresenter scorePresenter_Bot;

    private GameSessionPresenter gameSessionPresenter;

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

        gameSessionPresenter = new GameSessionPresenter
            (new GameSessionModel(), 
            viewContainer.GetView<GameSessionView>());
        gameSessionPresenter.Initialize();



        diceRollPresenter_Me = new DiceRollPresenter
            (new DiceRollModel(5, 3),
            viewContainer.GetView<DiceRollView>("Me"));
        diceRollPresenter_Me.Initialize();

        yatzyCombinationPresenter_Me = new YatzyCombinationPresenter
            (new YatzyCombinationModel(13),
            viewContainer.GetView<YatzyCombinationView>("Me"));
        yatzyCombinationPresenter_Me.Initialize();

        scorePresenter_Me = new ScorePresenter
            (new ScoreModel(soundPresenter),
            viewContainer.GetView<ScoreView>("Me"));
        scorePresenter_Me.Initialize();




        diceRollPresenter_Bot = new DiceRollPresenter
            (new DiceRollModel(5, 3),
            viewContainer.GetView<DiceRollView>("Bot"));
        diceRollPresenter_Bot.Initialize();

        yatzyCombinationPresenter_Bot = new YatzyCombinationPresenter
            (new YatzyCombinationModel(13),
            viewContainer.GetView<YatzyCombinationView>("Bot"));
        yatzyCombinationPresenter_Bot.Initialize();

        scorePresenter_Bot = new ScorePresenter
            (new ScoreModel(soundPresenter),
            viewContainer.GetView<ScoreView>("Bot"));
        scorePresenter_Bot.Initialize();


        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        ActivateEvents();

        sceneRoot.Activate();
    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();
        //Me
        diceRollPresenter_Me.OnGetAllDiceValues += yatzyCombinationPresenter_Me.SetNumbersCombination;
        diceRollPresenter_Me.OnStartRoll += yatzyCombinationPresenter_Me.Deactivate;
        diceRollPresenter_Me.OnStopRoll += yatzyCombinationPresenter_Me.Activate;
        diceRollPresenter_Me.OnStartRoll += diceRollPresenter_Me.DeactivateFreezeToggle;
        diceRollPresenter_Me.OnStopRoll += diceRollPresenter_Me.ActivateFreezeToggle;

        //yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += 
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += gameSessionPresenter.ChangeToSecondUser;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += diceRollPresenter_Me.Reload;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += yatzyCombinationPresenter_Me.Deactivate;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination += diceRollPresenter_Me.DeactivateFreezeToggle;
        yatzyCombinationPresenter_Me.OnGetScore += scorePresenter_Me.AddScore;



        //Bot
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += gameSessionPresenter.ChangeToFirstUser;
        diceRollPresenter_Bot.OnGetAllDiceValues += yatzyCombinationPresenter_Bot.SetNumbersCombination;
        diceRollPresenter_Bot.OnStartRoll += yatzyCombinationPresenter_Bot.Deactivate;
        diceRollPresenter_Bot.OnStopRoll += yatzyCombinationPresenter_Bot.Activate;
        diceRollPresenter_Bot.OnStartRoll += diceRollPresenter_Bot.DeactivateFreezeToggle;
        diceRollPresenter_Bot.OnStopRoll += diceRollPresenter_Bot.ActivateFreezeToggle;

        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += diceRollPresenter_Bot.Reload;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += yatzyCombinationPresenter_Bot.Deactivate;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination += diceRollPresenter_Bot.DeactivateFreezeToggle;
        yatzyCombinationPresenter_Bot.OnGetScore += scorePresenter_Bot.AddScore;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        diceRollPresenter_Me.OnGetAllDiceValues -= yatzyCombinationPresenter_Me.SetNumbersCombination;

        diceRollPresenter_Me.OnStartRoll -= yatzyCombinationPresenter_Me.Deactivate;
        diceRollPresenter_Me.OnStopRoll -= yatzyCombinationPresenter_Me.Activate;
        diceRollPresenter_Me.OnStartRoll -= diceRollPresenter_Me.DeactivateFreezeToggle;
        diceRollPresenter_Me.OnStopRoll -= diceRollPresenter_Me.ActivateFreezeToggle;

        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= diceRollPresenter_Me.Reload;
        yatzyCombinationPresenter_Bot.OnFreezeYatzyCombination -= yatzyCombinationPresenter_Bot.Deactivate;
        yatzyCombinationPresenter_Me.OnFreezeYatzyCombination -= diceRollPresenter_Me.DeactivateFreezeToggle;

        yatzyCombinationPresenter_Me.OnGetScore -= scorePresenter_Me.AddScore;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromFinishPanel += HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromFinishPanel += HandleGoToBotGame;
        yatzyCombinationPresenter_Me.OnFinishGame += sceneRoot.OpenFinishPanel;

        diceRollPresenter_Me.OnLoseFirstAttempt += sceneRoot.OpenPlayRollPanel_Me;

        diceRollPresenter_Bot.OnLoseFirstAttempt += sceneRoot.OpenPlayRollPanel_Bot;

        gameSessionPresenter.OnChangedToFirstUser += sceneRoot.OpenGamePanel_Me;
        gameSessionPresenter.OnChangedToFirstUser += sceneRoot.OpenRollPanel_Me;
        gameSessionPresenter.OnChangedToSecondUser += sceneRoot.OpenGamePanel_Bot;
        gameSessionPresenter.OnChangedToSecondUser += sceneRoot.OpenRollPanel_Bot;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToGoMainMenuFromMainPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoMainMenuFromFinishPanel -= HandleGoToMainMenu;
        sceneRoot.OnClickToGoSoloGameFromFinishPanel -= HandleGoToBotGame;
        yatzyCombinationPresenter_Me.OnFinishGame -= sceneRoot.OpenFinishPanel;

        diceRollPresenter_Me.OnGetFullAttempt -= sceneRoot.OpenRollPanel_Me;
        diceRollPresenter_Me.OnLoseFirstAttempt -= sceneRoot.OpenPlayRollPanel_Me;

        diceRollPresenter_Bot.OnGetFullAttempt -= sceneRoot.OpenRollPanel_Bot;
        diceRollPresenter_Bot.OnLoseFirstAttempt -= sceneRoot.OpenPlayRollPanel_Bot;

        gameSessionPresenter.OnChangedToFirstUser -= sceneRoot.OpenGamePanel_Me;
        gameSessionPresenter.OnChangedToSecondUser -= sceneRoot.OpenGamePanel_Bot;
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

        diceRollPresenter_Me?.Dispose();
        yatzyCombinationPresenter_Me?.Dispose();
        scorePresenter_Me?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnGoToMainMenu;
    public event Action OnGoToBotGame;

    private void HandleGoToMainMenu()
    {
        Deactivate();
        OnGoToMainMenu?.Invoke();
    }

    private void HandleGoToBotGame()
    {
        Deactivate();
        OnGoToBotGame?.Invoke();
    }

    #endregion
}
