using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthenticationView : View
{
    public event Action<string> OnClickSignUpButton;
    public event Action OnClickRandomNicknameButton;

    public event Action<string> OnRegisterLoginValueChanged;

    //[SerializeField] private List<Image>

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TMP_InputField fieldLoginRegistration;
    [SerializeField] private GameObject registerButtonObject;
    [SerializeField] private Button registrationButton;
    //[SerializeField] private Button randomNicknameButton;

    [SerializeField] private TextMeshProUGUI textDescriptionRegisterLogin;

    public void Initialize()
    {
        registrationButton.onClick.AddListener(HandlerClickToRegistrationButton);
        //randomNicknameButton.onClick.AddListener(HandlerClickToRandomNicknameButton);
        fieldLoginRegistration.onValueChanged.AddListener(HandlerOnRegisterLoginValueChanged);
    }

    public void Dispose()
    {
        registrationButton.onClick.RemoveListener(HandlerClickToRegistrationButton);
        //randomNicknameButton.onClick.RemoveListener(HandlerClickToRandomNicknameButton);
        fieldLoginRegistration.onValueChanged.RemoveListener(HandlerOnRegisterLoginValueChanged);
    }

    public void OnCorrectRegisterLogin()
    {
        registerButtonObject.SetActive(true);

        //textDescriptionRegisterLogin.text = "";
    }

    public void OnIncorrectRegisterLogin(string textError)
    {
        registerButtonObject.SetActive(false);

        //textDescriptionRegisterLogin.text = textError;
    }

    public void DisplayRandomNickname(string text)
    {
        fieldLoginRegistration.text = text;
    }

    public void GetMessage(string message)
    {
        //textDescription.text = message;
    }

    #region Input

    //private void HandlerClickToRandomNicknameButton()
    //{
    //    OnClickRandomNicknameButton?.Invoke();
    //}

    private void HandlerClickToRegistrationButton()
    {
        string login = fieldLoginRegistration.text;

        OnClickSignUpButton?.Invoke(login);
    }

    private void HandlerOnRegisterLoginValueChanged(string value)
    {
        OnRegisterLoginValueChanged?.Invoke(value);
    }

    #endregion
}
