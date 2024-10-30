using UnityEngine;

public class YatzyCombinationData
{
    public int Index { get; private set; }
    public int Score { get; private set; }
    public bool IsSelect { get; private set; }
    public bool IsFreeze { get; private set; }

    public YatzyCombinationData(int index, bool select = false)
    {
        Index = index;
        IsSelect = select;
    }

    public void SetScore(int score)
    {
        Score = score;
    }

    public void SetSelect(bool select)
    {
        IsSelect = select;
    }

    public void SetFreeze(bool freeze)
    {
        IsFreeze = freeze;
    }
}
