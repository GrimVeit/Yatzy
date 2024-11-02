using System;
using UnityEngine;

public class ScoreModel
{
    public event Action<int> OnChangeScoreForBonus;

    public event Action<int> OnTakeResult;
    public event Action<int> OnChangeScore_Value;
    public event Action OnChangeScore;

    private int record;
    private int currentRecord = 0;

    private ISoundProvider soundProvider;

    private int maxScoreForBonus = 63;
    private int currentScoreForBonus = 0;
    private bool isGetBonus = false;
     
    public ScoreModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        record = PlayerPrefs.GetInt(PlayerPrefsKeys.GAME_RECORD);
    }


    public void Dispose()
    {
        if (currentRecord > record)
        {
            record = currentRecord;
            PlayerPrefs.SetInt(PlayerPrefsKeys.GAME_RECORD, record);
        }
    }
    
    public void AddScore(int score, bool isNumbersOnly)
    {
        if (isNumbersOnly)
        {
            currentScoreForBonus += score;
            OnChangeScoreForBonus?.Invoke(currentScoreForBonus);

            if(currentScoreForBonus >= maxScoreForBonus && !isGetBonus)
            {
                Debug.Log("анмся онксвем");
                currentRecord += 35;
                isGetBonus = true;
            }
        }

        currentRecord += score;
        OnChangeScore?.Invoke();
        OnChangeScore_Value?.Invoke(currentRecord);
    }

    public void TakeResult()
    {
        OnTakeResult?.Invoke(currentRecord);
    }
}
