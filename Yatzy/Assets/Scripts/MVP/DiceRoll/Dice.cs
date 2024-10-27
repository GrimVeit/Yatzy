using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public event Action<int, DiceData> OnStopRotated;

    [SerializeField] private List<Sprite> spritesAnimation = new List<Sprite>();
    [SerializeField] private List<DiceData> diceDatas = new List<DiceData>();

    [SerializeField] private float minDurationRotation;
    [SerializeField] private float maxDurationRotation;
    [SerializeField] private float minTimeReload;
    [SerializeField] private float maxTimeReload;
    [SerializeField] private Image diceImage;

    private IEnumerator rollCoroutine;

    private int currentIndexDice;

    public void Initialize(int index)
    {
        currentIndexDice = index;
    }

    public void Dispose()
    {

    }

    public void Roll()
    {
        if (rollCoroutine != null)
            Coroutines.Stop(rollCoroutine);

        rollCoroutine = Roll_Coroutine();
        Coroutines.Start(rollCoroutine);
    }

    private IEnumerator Roll_Coroutine()
    {
        float elapsedTime = 0;
        float timeReload = UnityEngine.Random.Range(minTimeReload, maxTimeReload);
        float durationRotation = UnityEngine.Random.Range(minDurationRotation, maxDurationRotation);

        while(elapsedTime < durationRotation)
        {
            diceImage.sprite = spritesAnimation[UnityEngine.Random.Range(0, spritesAnimation.Count)];

            yield return new WaitForSeconds(timeReload);

            elapsedTime += timeReload;
        }

        DiceData randomData = diceDatas[UnityEngine.Random.Range(0, diceDatas.Count)];
        diceImage.sprite = randomData.DiceSprite;

        OnStopRotated?.Invoke(currentIndexDice, randomData);
    }
}
