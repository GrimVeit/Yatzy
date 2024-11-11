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

    [SerializeField] private List<Sprite> spritesAvatar = new List<Sprite>();

    [Space]
    [Space]
    [Header("ACCOUNT DATA")]
    public int Avatar;
    public string Nickname;

    private List<UserGrid> spawnUsers = new List<UserGrid>();


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

    public void DisplayUsersRecords(List<UserData> users)
    {
        for (int i = 0; i < contentUsers.childCount; i++)
        {
            spawnUsers.Clear();
            Destroy(contentUsers.GetChild(i).gameObject);
        }

        //users = users.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        users = users.OrderByDescending(entry => entry.Record).ToList();

        foreach (var item in users)
        {
            UserGrid grid = Instantiate(userGridPrefab, contentUsers);
            grid.SetData(item.Nickname, item.Record, spritesAvatar[int.Parse(item.Avatar)]);
            spawnUsers.Add(grid);
        }
    }

    public void TryChangeAvatarInSpawnUsers(string nickname, int avatar)
    {
        for (int i = 0; i < spawnUsers.Count; i++)
        {
            if (spawnUsers[i].Nickname == nickname)
            {
                spawnUsers[i].SetAvatar(spritesAvatar[avatar]);
            }
        }
    }

    #region Input

    private void HandlerClickToChangeAvatarButton()
    {
        OnChangeAvatar?.Invoke();
    }

    #endregion
}
