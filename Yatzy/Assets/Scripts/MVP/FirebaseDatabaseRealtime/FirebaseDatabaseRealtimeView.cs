using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseRealtimeView : View
{
    public event Action OnChangeAvatar;

    [SerializeField] private Transform contentUsers;
    [SerializeField] private UserGrid userGridPrefab;

    [SerializeField] private Button buttonChangeAvatar;

    [Space]
    [Space]
    [Header("ACCOUNT DATA")]
    public int Avatar;
    public string Nickname;


    public void Initialize()
    {
        buttonChangeAvatar.onClick.AddListener(HandlerClickToChangeAvatarButton);
    }

    public void Dispose()
    {
        buttonChangeAvatar.onClick.RemoveListener(HandlerClickToChangeAvatarButton);
    }

    public void TestDebugAvatar(int index)
    {
        Avatar = index;
    }

    public void TestDebugNickname(string nickname)
    {
        Nickname = nickname;
    }

    public void DisplayUsersRecords(Dictionary<string, int> users)
    {
        for (int i = 0; i < contentUsers.childCount; i++)
        {
            Destroy(contentUsers.GetChild(i).gameObject);
        }

        //users = users.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        users = users.OrderByDescending(entry => entry.Value).ToDictionary(x => x.Key, x => x.Value);

        foreach (var item in users)
        {
            UserGrid grid = Instantiate(userGridPrefab, contentUsers);
            grid.SetData(item.Key, item.Value.ToString());
        }
    }

    #region Input

    private void HandlerClickToChangeAvatarButton()
    {
        OnChangeAvatar?.Invoke();
    }

    #endregion
}
