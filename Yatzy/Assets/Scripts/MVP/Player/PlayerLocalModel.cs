using System;

public class PlayerLocalModel : IPlayerModel
{
    public event Action<string> OnGetNickname;
    public event Action<int> OnGetAvatarIndex;

    public string Nickname { get; private set; }
    public int AvatarIndex { get; private set; }

    public void Initialize()
    {

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
        AvatarIndex = avatarIndex;
        OnGetAvatarIndex?.Invoke(AvatarIndex);
    }
}
