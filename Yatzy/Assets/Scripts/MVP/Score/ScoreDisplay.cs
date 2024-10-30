using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private GameObject displayScore;

    private Vector3 defaultDisplayScoreSize;

    public void Initialize()
    {
        defaultDisplayScoreSize = displayScore.transform.localScale;
    }

    public void Dispose()
    {

    }

    public void DisplayScore(int coins)
    {
        textCoins.text = coins.ToString();
    }

    public void ShakeDisplay()
    {
        displayScore.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).
                    OnComplete(() => displayScore.transform.DOScale(defaultDisplayScoreSize, 0.2f));
    }
}
