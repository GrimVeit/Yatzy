using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthenticationView : View
{
    public event Action OnClickSignUpButton;

    [SerializeField] private GameObject registerButtonObject;
    [SerializeField] private Button registrationButton;

    [SerializeField] private TextMeshProUGUI textDescription;

    public void Initialize()
    {
        registrationButton.onClick.AddListener(HandlerClickToRegistrationButton);
    }

    public void Dispose()
    {
        registrationButton.onClick.RemoveListener(HandlerClickToRegistrationButton);
    }

    public void ActivateRegistrationButton()
    {
        registerButtonObject.SetActive(true);
    }

    public void DeactivateRegistrationButton()
    {
        registerButtonObject.SetActive(false);
    }

    public void SetDescription(string description)
    {
        textDescription.text = description;
    }

    #region Input

    private void HandlerClickToRegistrationButton()
    {
        OnClickSignUpButton?.Invoke();
    }

    #endregion
}
