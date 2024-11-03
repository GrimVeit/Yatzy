using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseRealtimeView : View
{
    public event Action<int> OnChooseImage;

    [SerializeField] private Transform contentUsers;
    [SerializeField] private UserGrid userGridPrefab;

    [SerializeField] private List<ImageElement> imageElements = new List<ImageElement>();


    [SerializeField] private List<TextMeshProUGUI> nicknames = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> avatars = new List<Image>();
    [SerializeField] private List<Sprite> avatarsSprites = new List<Sprite>();

    public void Initialize()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage += HandlerClickToChooseImage;
            imageElements[i].Initialize(i);
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < imageElements.Count; i++)
        {
            imageElements[i].OnChooseImage -= HandlerClickToChooseImage;
            imageElements[i].Dispose();
        }
    }

    public void Select(int index)
    {
        imageElements[index].Select();
    }

    public void Deselect(int index)
    {
        imageElements[index].Deselect();
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

    public void NicknameDisplay(string nick)
    {
        for (int i = 0; i < nicknames.Count; i++)
        {
            nicknames[i].text = nick;
        }
    }

    public void AvatarDisplay(int index)
    {
        for (int i = 0; i < avatars.Count; i++)
        {
            avatars[i].sprite = avatarsSprites[index];
        }
    }


    #region Input

    private void HandlerClickToChooseImage(int index)
    {
        OnChooseImage?.Invoke(index);
    }

    #endregion
}
