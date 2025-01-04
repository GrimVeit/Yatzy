using System;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class CountryCheckerSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UICountryCheckerSceneRoot sceneRootPrefab;

    private UICountryCheckerSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private GeoLocationPresenter geoLocationPresenter;
    private InternetPresenter internetPresenter;
    private SoundPresenter soundPresenter;

    private FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter;

    public void Run(UIRootView uIRootView)
    {
        Debug.Log("OPEN COUNTRY CHECKER SCENE");

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
                soundPresenter.Initialize();

                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                firebaseDatabaseRealtimePresenter = new FirebaseDatabaseRealtimePresenter
                (new FirebaseDatabaseRealtimeModel(firebaseAuth, databaseReference, soundPresenter),
                    viewContainer.GetView<FirebaseDatabaseRealtimeView>());

                geoLocationPresenter = new GeoLocationPresenter(new GeoLocationModel());

                Debug.Log("Success");

                internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());
                internetPresenter.Initialize();

                Debug.Log("Success");

                ActivateActions();

                Debug.Log("Success");

                firebaseDatabaseRealtimePresenter.Initialize();

                Debug.Log("Success");

                firebaseDatabaseRealtimePresenter.GetUserFromPlace(1);

                //internetPresenter.StartCkeckInternet();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    public void Dispose()
    {
        DeactivateActions();

        internetPresenter?.Dispose();
    }

    private void ActivateActions()
    {
        firebaseDatabaseRealtimePresenter.OnGetUserFromPlace += CheckUser;
        internetPresenter.OnInternetAvailable += geoLocationPresenter.GetUserCountry;
        internetPresenter.OnInternetUnavailable += TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry += ActivateSceneInCountry;
    }

    private void DeactivateActions()
    {
        firebaseDatabaseRealtimePresenter.OnGetUserFromPlace -= CheckUser;
        internetPresenter.OnInternetAvailable -= geoLocationPresenter.GetUserCountry;
        internetPresenter.OnInternetUnavailable -= TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry -= ActivateSceneInCountry;
    }

    private void CheckUser(UserData userData)
    {
        Debug.Log(userData.Nickname);

        if(userData.Nickname == "GOGOGO")
        {
            internetPresenter.StartCkeckInternet();
        }
        else
        {
            TransitionToMainMenu();
        }
    }

    private void ActivateSceneInCountry(string country)
    {
        switch (country)
        {
            case "AU":
                TransitionToOther();
                break;
            case "DE":
                TransitionToOther();
                break;
            case "IT":
                TransitionToOther();
                break;
            case "AT":
                TransitionToOther();
                break;
            case "RU":
                TransitionToOther();
                break;
            default:
                TransitionToMainMenu();
                break;
        }
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToOther;

    private void TransitionToMainMenu()
    {
        Dispose();
        Debug.Log("NO GOOD STRANA");
        GoToMainMenu?.Invoke();
    }

    private void TransitionToOther()
    {
        Dispose();
        Debug.Log("GOOD STRANA");
        GoToOther?.Invoke();
    }

    #endregion
}
