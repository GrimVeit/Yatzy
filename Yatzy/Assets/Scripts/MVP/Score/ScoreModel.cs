using System;
using UnityEngine;

public class ScoreModel
{
    public event Action<int> OnTakeResult;
    public event Action<int> OnChangeScore_Value;
    public event Action OnChangeScore;

    private int record;
    private int currentRecord = 0;

    private ISoundProvider soundProvider;
     
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
    
    public void AddScore(int score)
    {
        currentRecord += score;
        OnChangeScore?.Invoke();
        OnChangeScore_Value?.Invoke(currentRecord);
    }

    public void TakeResult()
    {
        OnTakeResult?.Invoke(currentRecord);
    }
}
