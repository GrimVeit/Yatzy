using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : View, IIdentify
{
    public string GetID() => idComponent;

    [SerializeField] private string idComponent;

    [SerializeField] private List<ScoreDisplay> scoreDisplays = new List<ScoreDisplay>();
    [SerializeField] private TextMeshProUGUI textScoreBonus;

    private Vector3 defaultDisplayScoreSize;

    public void Initialize()
    {
        for (int i = 0; i < scoreDisplays.Count; i++)
        {
            scoreDisplays[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < scoreDisplays.Count; i++)
        {
            scoreDisplays[i].Dispose();
        }
    }

    public void DisplayScore(int coins)
    {
        for (int i = 0; i < scoreDisplays.Count; i++)
        {
            scoreDisplays[i].DisplayScore(coins);
        }
    }

    public void DisplayScoreBonus(int score)
    {
        textScoreBonus.text = (score + "/63").ToString();
    }

    public void ShakeDisplay()
    {
        for (int i = 0; i < scoreDisplays.Count; i++)
        {
            scoreDisplays[i].ShakeDisplay();
        }
    }

}
