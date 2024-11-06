using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseRealtimeModel
{
    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatar;

    public event Action<Dictionary<string, int>> OnGetUsersRecords;

    public string Nickname { get; private set; }
    public int Record { get; private set; }
    public int Avatar { get; private set; }

    private Dictionary<string, int> userRecordsDictionary = new Dictionary<string, int>();


    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    public FirebaseDatabaseRealtimeModel(FirebaseAuth auth, DatabaseReference database)
    {
        this.auth = auth;
        this.databaseReference = database;
    }

    public void Initialize()
    {
        Record = PlayerPrefs.GetInt(PlayerPrefsKeys.GAME_RECORD, 0);

        if (auth.CurrentUser != null)
        {
            SaveChangesToServer();
            DisplayUsersRecords();
        }
    }

    public void CreateNewAccountInServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        PlayerPrefs.SetInt(PlayerPrefsKeys.GAME_RECORD, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.NICKNAME, Nickname);
        PlayerPrefs.SetInt(PlayerPrefsKeys.IMAGE_INDEX, Avatar);
        UserData user = new(Nickname, 0, Avatar);
        string json = JsonUtility.ToJson(user);

        OnGetNickname?.Invoke(Nickname);

        Debug.Log(Nickname + "/" + Avatar);

        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
        OnGetNickname?.Invoke(Nickname);
    }

    public void SetAvatar(int avatarIndex)
    {
        Avatar = avatarIndex;
        OnGetAvatar?.Invoke(Avatar);
    }

    public void SaveChangesToServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        UserData user = new(Nickname, Record, Avatar);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    #region Records

    public void DisplayUsersRecords()
    {
        Coroutines.Start(GetUsersRecords());
    }

    private IEnumerator GetUsersRecords()
    {
        var task = databaseReference.Child("Users").OrderByChild("Coins").LimitToFirst(15).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Error display record");
            yield break;
        }

        userRecordsDictionary.Clear();

        DataSnapshot data = task.Result;

        Debug.Log("Success " + data.ChildrenCount);

        foreach (var user in data.Children)
        {
            string name = user.Child("Nickname").Value.ToString();
            string coins = user.Child("Record").Value.ToString();
            userRecordsDictionary.Add(name, int.Parse(coins));
        }

        OnGetUsersRecords?.Invoke(userRecordsDictionary);
    }

    #endregion
}

public class UserData
{
    public string Nickname;
    public int Avatar;
    public int Record;

    public UserData(string nickname, int record, int avatar)
    {
        Nickname = nickname;
        Record = record;
        Avatar = avatar;
    }
}
