using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToSoloGame_Action += () => coroutines.StartCoroutine(LoadAndStartGameSoloScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameSoloScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_SOLO);
        yield return null;

        var sceneEntryPoint = Object.FindObjectOfType<GameSoloSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu_Action += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameBotScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_BOT);
        yield return null;

        //var sceneEntryPoint = Object.FindObjectOfType<RouletteEntryPoint>();
        //sceneEntryPoint.Run(rootView);

        //sceneEntryPoint.GoToMainMenu_Action += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameFriendScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_FRIEND);
        yield return null;

        //var sceneEntryPoint = Object.FindObjectOfType<RouletteEntryPoint>();
        //sceneEntryPoint.Run(rootView);

        //sceneEntryPoint.GoToMainMenu_Action += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("�������� ����� - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
