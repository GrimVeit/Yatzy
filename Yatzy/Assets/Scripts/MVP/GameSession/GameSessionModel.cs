using System;
using UnityEngine;

public class GameSessionModel : MonoBehaviour
{
    public event Action OnChangedToSecondUser;
    public event Action OnChangedToFirstUser;

    public void ChangeToFirstUser()
    {
        OnChangedToFirstUser?.Invoke();
    }

    public void ChangeToSecondUser()
    {
        OnChangedToSecondUser?.Invoke();
    }
}
