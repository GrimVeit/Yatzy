using System;

public class ScorePresenter
{
    private ScoreModel scoreModel;
    private ScoreView scoreView;

    public ScorePresenter(ScoreModel scoreModel, ScoreView scoreView)
    {
        this.scoreModel = scoreModel;
        this.scoreView = scoreView;
    }

    public void Initialize()
    {
        ActivateEvents();

        scoreModel.Initialize();
        scoreView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        scoreModel.Dispose();
        scoreView.Dispose();
    }

    private void ActivateEvents()
    {
        scoreModel.OnChangeScoreForBonus += scoreView.DisplayScoreBonus;
        scoreModel.OnChangeScore_Value += scoreView.DisplayScore;
        scoreModel.OnChangeScore += scoreView.ShakeDisplay;
    }

    private void DeactivateEvents()
    {
        scoreModel.OnChangeScoreForBonus -= scoreView.DisplayScoreBonus;
        scoreModel.OnChangeScore_Value -= scoreView.DisplayScore;
        scoreModel.OnChangeScore -= scoreView.ShakeDisplay;
    }

    #region Input

    public event Action<int> OnTakeResult
    {
        add { scoreModel.OnTakeResult += value; }
        remove { scoreModel.OnTakeResult -= value; }
    }

    public void AddScore(int score, bool isNumbersOnly)
    {
        scoreModel.AddScore(score, isNumbersOnly);
    }

    public void TakeResult()
    {
        scoreModel.TakeResult();
    }

    #endregion
}
