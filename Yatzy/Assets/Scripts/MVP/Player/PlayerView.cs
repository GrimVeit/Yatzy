using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View, IPlayerView
{
    [SerializeField] private List<TextMeshProUGUI> textNicknames;
    [SerializeField] private List<Image> imageAvatars;

    [SerializeField] private List<Sprite> spritesAvatars;

    public event Action<string> OnChooseNickname = null;
    public event Action<int> OnChooseImage = null;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void ChooseAvatar(int index)
    {
        for (int i = 0; i < imageAvatars.Count; i++)
        {
            imageAvatars[i].sprite = spritesAvatars[index];
        }
    }

    public void ChooseNickname(string nickname)
    {
        for (int i = 0; i < textNicknames.Count; i++)
        {
            textNicknames[i].text = nickname;
        }
    }
}
