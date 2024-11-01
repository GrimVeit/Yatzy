using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : View
{
    [SerializeField] private List<TextMeshProUGUI> textNicknames;
    [SerializeField] private List<Image> imageAvatars;

    [SerializeField] private List<Sprite> spritesAvatars;

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
