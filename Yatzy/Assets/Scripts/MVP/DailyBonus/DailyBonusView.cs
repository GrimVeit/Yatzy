using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusView : View
{
    public event Action OnClickSpinButton;
    public event Action<int> OnGetBonus;
    public event Action<float> OnSpin;

    [SerializeField] private List<Bonus> bonuses = new List<Bonus>();

    [SerializeField] private Transform content;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI textCoins; 
    [SerializeField] private Button buttonDailyBonus;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minScrollSpeed;
    [SerializeField] private float maxScrollSpeed;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    private Vector3 normalScaleBonusButton;
    private Tween scaleTween;

    private IEnumerator spin_Coroutine;

    public void Initialize()
    {
        normalScaleBonusButton = buttonDailyBonus.transform.localScale;

        buttonDailyBonus.onClick.AddListener(HandlerClickToSpinButton);
    }

    public void Dispose()
    {
        buttonDailyBonus.onClick.RemoveListener(HandlerClickToSpinButton);
    }

    public void DeactivateSpinButton() 
    {
        buttonDailyBonus.gameObject.SetActive(false);

        if (scaleTween != null)
            scaleTween.Kill();
        buttonDailyBonus.transform.localScale = normalScaleBonusButton;
    }

    public void ActivateSpinButton()
    {
        buttonDailyBonus.gameObject.SetActive(true);

        scaleTween = buttonDailyBonus.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void DisplayCoins(int coins)
    {
        textCoins.text = coins.ToString();
    }

    public void StartSpin()
    {
        if (spin_Coroutine != null)
            Coroutines.Stop(spin_Coroutine);

        spin_Coroutine = Spin();
        Coroutines.Start(spin_Coroutine);
    }

    //private IEnumerator RotateSpin_Coroutine()
    //{
    //    float elapsedTime = 0f;
    //    float startSpeed = UnityEngine.Random.Range(minSpinSpeed, maxSpinSpeed);
    //    float duration = UnityEngine.Random.Range(minDuration, maxDuration);
    //    float endSpeed = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);

    //        //scrollRect.verticalNormalizedPosition += currentSpeed * Time.deltaTime;
    //        //scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition % 1; // Зацикливание

    //        spinTransform.Rotate(spinVector * currentSpeed * Time.deltaTime);

    //        yield return null;
    //    }

    //    Bonus bonus = GetClosestBonus();
    //    Debug.Log(bonus.Coins);
    //    OnGetBonus?.Invoke(bonus.Coins);
    //}

    private IEnumerator Spin()
    {
        float elapsedTime = 0f;
        float startSpeed = UnityEngine.Random.Range(minScrollSpeed, maxScrollSpeed);
        float duration = UnityEngine.Random.Range(minDuration, maxDuration);
        float endSpeed = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);

            OnSpin?.Invoke(currentSpeed);

            scrollRect.verticalNormalizedPosition += currentSpeed * Time.deltaTime;
            scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition % 1; // Зацикливание


            yield return null;
        }


        Bonus bonus = GetClosestBonus();

        yield return StartCoroutine(SmoothScrollToItem(bonus.TransformBonus));

        Debug.Log(bonus.Coins);
        OnGetBonus?.Invoke(bonus.Coins);

    }

    private Bonus GetClosestBonus()
    {
        float minDistance = float.MaxValue;
        Bonus closestBonus = null;


        foreach (var bonus in bonuses)
        {
            float distance = Vector2.Distance(bonus.TransformBonus.position, centerPoint.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestBonus = bonus;
            }
        }


        return closestBonus;
    }

    private IEnumerator SmoothScrollToItem(Transform item)
    {
        float distance = item.position.y - centerPoint.position.y;
        Vector3 targetPosition = content.position + new Vector3(0, -distance, 0);
        float elapsedTime = 0f;
        float smoothDuration = 0.3f;


        while (elapsedTime < smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            content.position = Vector3.Lerp(content.position, targetPosition, elapsedTime / smoothDuration);
            yield return null;
        }

        yield return null;

        content.position = targetPosition;
    }

    private void HandlerClickToSpinButton()
    {
        OnClickSpinButton?.Invoke();
    }
}
