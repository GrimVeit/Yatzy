using System;
using System.Reflection;

public class PlayerLocalModel : IPlayerModel
{
    public event Action<int> OnSelectIndex;
    public event Action<int> OnDeselectIndex;

    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatarIndex;

    public string Nickname { get; private set; } = null;
    public int AvatarIndex { get; private set; } = 0;

    public void Initialize()
    {
        OnGetAvatarIndex?.Invoke(AvatarIndex);
        OnGetNickname?.Invoke(Nickname);
    }

    public void Dispose()
    {

    }

    public void OnChangeNickname(string nickname)
    {
        Nickname = nickname;
        OnGetNickname?.Invoke(Nickname);
    }

    public void OnChangeAvatar(int avatarIndex)
    {
        if (AvatarIndex == avatarIndex) return;

        if (AvatarIndex != avatarIndex)
        {
            OnDeselectIndex?.Invoke(AvatarIndex);
        }

        AvatarIndex = avatarIndex;
        OnSelectIndex?.Invoke(AvatarIndex);
        OnGetAvatarIndex?.Invoke(AvatarIndex);
    }
}
