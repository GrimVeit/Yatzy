using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NicknameView : View
{
    public event Action OnSubmitNickname;
    public event Action<string> OnChangeNickname;

    [SerializeField] private List<TextMeshProUGUI> textNicknames = new List<TextMeshProUGUI>();

    [SerializeField] private TMP_InputField inputFieldNickname;

    public void Initialize()
    {
        inputFieldNickname.onValueChanged.AddListener(HandlerOnNicknameTextValueChanged);
    }

    public void Dispose()
    {
        inputFieldNickname.onValueChanged.RemoveListener(HandlerOnNicknameTextValueChanged);
    }

    public void ChangeNickname(string nickname)
    {
        for (int i = 0; i < textNicknames.Count; i++)
        {
            textNicknames[i].text = nickname;
        }
    }

    #region Input

    private void HandlerOnNicknameTextValueChanged(string value)
    {
        OnChangeNickname?.Invoke(value);
    }

    #endregion
}
