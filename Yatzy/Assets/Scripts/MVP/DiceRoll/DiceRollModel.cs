using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceRollModel
{
    public event Action<int[]> OnGetAllDiceValues;

    //Фриз / анфриз кубиков
    public event Action<int> OnFreeseDice;
    public event Action<int> OnUnfreeseDice;

    //Попытки кручения кубиков
    public event Action OnAttemptAvailable;
    public event Action OnAttemptUnvailable;
    public event Action<int> OnChangedAttemptsCount;

    public event Action OnActivateRoll;
    public event Action OnDeactivateRoll;

    //Запуск / отключение кручения
    public event Action<int[]> OnStartRoll_Indexes;
    public event Action OnStartRoll;
    public event Action OnStopRoll;

    private Dictionary<int, DiceData> dices = new Dictionary<int, DiceData>();

    private int rolledDiceCount;

    private int diceRollCurrentAttempt;


    private int fullRollAttemptCount;
    private int fullDiceCount;

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
    }

    public void Dispose()
    {

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

        Debug.Log("Индекс кубика - " + index + ", Значение кубика - " + diceData.Number);

        if(rolledDiceCount == 0)
        {
            Debug.Log("Все кубики остановились");

            OnGetAllDiceValues?.Invoke(dices.Values.Select(d => d.Number).ToArray());

            OnStopRoll?.Invoke();

            if (!IsAvailableAttempts())
                OnDeactivateRoll?.Invoke();
            else
                OnActivateRoll?.Invoke();
        }
    }

    public void Reload()
    {
        ChangeAttempts(fullRollAttemptCount);
    }

    public void FreezeToggle(int index)
    {
        if (dices.ContainsKey(index))
        {
            if (dices[index].Frozen)
            {
                Debug.Log("Разморозка под индексом - "+ index);
                OnUnfreeseDice?.Invoke(index);
                dices[index].SetFrozen(false);
            }
            else
            {
                Debug.Log("Заморозка под индексом - " + index);
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
