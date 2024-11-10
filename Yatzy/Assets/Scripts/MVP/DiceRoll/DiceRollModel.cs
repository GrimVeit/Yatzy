using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRollModel
{
    public Dictionary<int, DiceData> dices { get; private set; } = new Dictionary<int, DiceData>();
    public event Action<int, int[]> OnGetAllDiceValues;

    //���� / ������ �������
    public event Action<int> OnFreeseDice;
    public event Action<int> OnUnfreeseDice;

    //������� �������� �������
    public event Action OnGetFullAttempt;
    public event Action OnLoseFirstAttempt;

    public event Action OnAttemptAvailable;
    public event Action OnAttemptUnvailable;
    public event Action<int> OnChangedAttemptsCount;

    public event Action OnActivateRoll;
    public event Action OnDeactivateRoll;

    //������ / ���������� ��������
    public event Action<int[]> OnStartRoll_Indexes;
    public event Action OnStartRoll;
    public event Action OnStopRoll;

    private int rolledDiceCount;

    private int diceRollCurrentAttempt;


    private int fullRollAttemptCount;
    private int fullDiceCount;

    private bool isActiveFreezeToggle;

    public DiceRollModel(int fullDiceCount, int fullRollAttempCount)
    {
        this.fullDiceCount = fullDiceCount;
        this.fullRollAttemptCount = fullRollAttempCount;

        for (int i = 0; i < this.fullDiceCount; i++)
        {
            dices[i] = new DiceData(false);
        }
    }

    public void Initialize()
    {
        ChangeAttempts(fullRollAttemptCount);
        OnGetFullAttempt?.Invoke();
    }

    public void Dispose()
    {

    }

    public void ActivateFreezeToggle()
    {
        isActiveFreezeToggle = true;
    }

    public void DeactivateFreezeToggle()
    {
        isActiveFreezeToggle = false;
    }

    public void StartRoll()
    {
        if (!IsAvailableAttempts()) return;

        ChangeAttempts(-1);

        int[] dicesIndex = GetUnfrozenDices();
        rolledDiceCount = dicesIndex.Length;
        OnStartRoll_Indexes?.Invoke(dicesIndex);
        OnStartRoll?.Invoke();
    }

    public void StopRoll(int index, DiceData diceData)
    {
        rolledDiceCount -= 1;

        dices[index] = diceData;

        //Debug.Log("������ ������ - " + index + ", �������� ������ - " + diceData.Number);

        if(rolledDiceCount == 0)
        {
            //Debug.Log("��� ������ ������������");

            OnGetAllDiceValues?.Invoke(diceRollCurrentAttempt, dices.Values.Select(d => d.Number).ToArray());

            OnStopRoll?.Invoke();

            if(fullRollAttemptCount - diceRollCurrentAttempt == 1)
                OnLoseFirstAttempt?.Invoke();

            if (!IsAvailableAttempts())
                OnDeactivateRoll?.Invoke();
            else
                OnActivateRoll?.Invoke();
        }
    }

    public void Reload()
    {
        AllUnfreeze();
        diceRollCurrentAttempt = 0;
        ChangeAttempts(fullRollAttemptCount);
        OnGetFullAttempt?.Invoke();
    }

    public void AllUnfreeze()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            if (dices[i].Frozen)
            {
                OnUnfreeseDice?.Invoke(i);
                dices[i].SetFrozen(false);
            }
        }
    }

    public void UnfreezeToggle(int index)
    {
        if (!isActiveFreezeToggle) return;

        if (dices[index].Frozen)
        {
            OnUnfreeseDice?.Invoke(index);
            dices[index].SetFrozen(false);
        }
    }

    public void FreezeToggle(int index)
    {
        if (!isActiveFreezeToggle) return;

        if (dices.ContainsKey(index))
        {
            if (dices[index].Frozen)
            {
                Debug.Log("���������� ��� �������� - "+ index);
                OnUnfreeseDice?.Invoke(index);
                dices[index].SetFrozen(false);
            }
            else
            {
                Debug.Log("��������� ��� �������� - " + index);
                OnFreeseDice?.Invoke(index);
                dices[index].SetFrozen(true);
            }
        }
    }

    private void ChangeAttempts(int attempts)
    {
        diceRollCurrentAttempt += attempts;

        Debug.Log(diceRollCurrentAttempt);

        if (diceRollCurrentAttempt <= 0)
        {
            diceRollCurrentAttempt = 0;
            OnChangedAttemptsCount?.Invoke(0);
            OnAttemptUnvailable?.Invoke();
            return;
        }

        OnChangedAttemptsCount?.Invoke(diceRollCurrentAttempt);
        OnAttemptAvailable?.Invoke();
    }

    private bool IsAvailableAttempts()
    {
        return diceRollCurrentAttempt > 0;
    }

    private int[] GetUnfrozenDices()
    {
        return dices.Where(pair => !pair.Value.Frozen).Select(pair => pair.Key).ToArray();
    }
}
