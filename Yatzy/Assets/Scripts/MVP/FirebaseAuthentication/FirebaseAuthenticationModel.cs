using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseAuthenticationModel
{
    public event Action OnActivate;
    public event Action OnDeactivate;


    public event Action<string> OnChangeUser;

    public event Action OnSignIn_Action;
    public event Action<string> OnSignInError_Action;

    public event Action OnSignUp_Action;
    public event Action<string> OnSignUpMessage_Action;

    public event Action OnSignOut_Action;

    public event Action OnDeleteAccount_Action;

    public event Action OnEnterRegisterLoginSuccess;
    public event Action<string> OnEnterRegisterLoginError;

    public event Action<string> OnGetRandomNickname;


    private FirebaseAuth auth;

    private readonly Regex mainRegex = new("^[a-zA-Z0-9._]*$");
    private readonly Regex invalidRegex = new(@"(\.{2,}|/{2,})");
    private const string URL = "https://dinoipsum.com/api/?format=text&paragraphs=1&words=1";

    public string Nickname;

    private ISoundProvider soundProvider;

    public FirebaseAuthenticationModel(FirebaseAuth auth, ISoundProvider soundProvider)
    {
        this.auth = auth;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        //auth = FirebaseAuth.DefaultInstance;
        //databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Activate()
    {
        OnActivate?.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
    }

    public bool CheckUserAuthentication()
    {
        if (auth.CurrentUser != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SignUp()
    {
        Debug.Log(Nickname);
        Coroutines.Start(SignUpCoroutine(Nickname + "@gmail.com", "123456"));
    }

    public void SignOut()
    {
        auth.SignOut();
        OnSignOut_Action?.Invoke();
        OnChangeUser?.Invoke(auth.CurrentUser.UserId);
    }

    public void DeleteAccount()
    {
        OnDeleteAccount_Action?.Invoke();
        Coroutines.Start(DeleteAuth_Coroutine());
    }

    #region Coroutines

    private IEnumerator SignInCoroutine(string emailTextValue, string passwordTextValue)
    {
        Task<AuthResult> task = auth.SignInWithEmailAndPasswordAsync(emailTextValue, passwordTextValue);

        yield return new WaitUntil(() => task.IsCompleted);
        yield return null;

        if (task.Exception != null)
        {
            OnSignInError_Action?.Invoke(task.Exception.Message);
            yield break;
        }

        //OnChangeUser?.Invoke();
        OnChangeUser?.Invoke(auth.CurrentUser.UserId);
        OnSignIn_Action?.Invoke();
    }

    private IEnumerator SignUpCoroutine(string emailTextValue, string passwordTextValue)
    {
        OnSignUpMessage_Action?.Invoke("Loading...");

        var task = auth.CreateUserWithEmailAndPasswordAsync(emailTextValue, passwordTextValue);

        yield return new WaitUntil(predicate: () => task.IsCompleted);
        yield return null;

        if (task.Exception != null)
        {
            Debug.Log("Не удалось создать аккаунт - " + task.Exception);
            OnSignUpMessage_Action?.Invoke(task.Exception.Message);
            yield break;
        }

        Debug.Log("Аккаунт создан");
        OnSignUpMessage_Action?.Invoke("Success!");
        OnChangeUser?.Invoke(auth.CurrentUser.UserId);
        OnSignUp_Action?.Invoke();

    }

    private IEnumerator DeleteAuth_Coroutine()
    {
        var task = auth.CurrentUser.DeleteAsync();

        yield return new WaitUntil(predicate: () => task.IsCompleted);

        if (task.Exception != null)
        {
            Debug.Log("Ошибка удаления аккаунта - " + task.Exception.Message);
            yield break;
        }

        SignOut();
    }

    #endregion
}
