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

    public event Action<List<UserData>> OnGetUsersRecords;
    public event Action<string, int> OnTryChangeLocalAvatar;

    public string Nickname { get; private set; }
    public int Record { get; private set; }
    public int Avatar { get; private set; }

    private List<UserData> userRecordsDictionary = new List<UserData>();


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
        UserData user = new(Nickname, 0.ToString(), Avatar.ToString());
        string json = JsonUtility.ToJson(user);

        OnGetNickname?.Invoke(Nickname);

        Debug.Log(Nickname + "/" + Avatar);

        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    public void ChangeLocalAvatar()
    {
        OnTryChangeLocalAvatar?.Invoke(Nickname, Avatar);
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
        UserData user = new(Nickname, Record.ToString(), Avatar.ToString());
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
            string record = user.Child("Record").Value.ToString();
            string avatar = user.Child("Avatar").Value.ToString();
            userRecordsDictionary.Add(new UserData(name, record, avatar));
        }

        OnGetUsersRecords?.Invoke(userRecordsDictionary);
    }

    #endregion
}

public class UserData
{
    public string Nickname;
    public string Avatar;
    public string Record;

    public UserData(string nickname, string record, string avatar)
    {
        Nickname = nickname;
        Record = record;
        Avatar = avatar;
    }
}
