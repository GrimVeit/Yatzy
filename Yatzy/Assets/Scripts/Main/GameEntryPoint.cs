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
        coroutines.StartCoroutine(LoadAndStartCountryChecker());
    }

    private IEnumerator LoadAndStartCountryChecker()
    {
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.COUNTRY_CHECKER);

        var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += ()=> coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());
    }

    private IEnumerator LoadAndStartOther()
    {
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.OTHER);

        var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();
        Debug.Log("TTTTTTT");

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToSoloGame_Action += () => coroutines.StartCoroutine(LoadAndStartGameSoloScene());
        sceneEntryPoint.GoToBotGame_Action += () => coroutines.StartCoroutine(LoadAndStartGameBotScene());
        sceneEntryPoint.GoToFriendGame_Action += () => coroutines.StartCoroutine(LoadAndStartGameFriendScene());

        yield return rootView.HideLoadingScreen();
        Debug.Log("FFFFFFF");
    }

    private IEnumerator LoadAndStartGameSoloScene()
    {
        rootView.SetLoadScreen(1);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_SOLO);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSoloSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnGoToSoloGame += () => coroutines.StartCoroutine(LoadAndStartGameSoloScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameBotScene()
    {
        rootView.SetLoadScreen(3);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_BOT);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameBotSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnGoToBotGame += () => coroutines.StartCoroutine(LoadAndStartGameBotScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameFriendScene()
    {
        rootView.SetLoadScreen(2);
        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.4f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_FRIEND);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameFriendSceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.OnGoToFriendGame += () => coroutines.StartCoroutine(LoadAndStartGameFriendScene());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
