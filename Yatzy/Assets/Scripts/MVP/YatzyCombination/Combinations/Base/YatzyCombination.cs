using System;
using UnityEngine;

public abstract class YatzyCombination : MonoBehaviour
{
    public Action<YatzyCombinationData> OnChooseCombination;

    public abstract void Initialize(YatzyCombinationData yatzyCombinationData);
    public virtual void Dispose() { }
    public abstract void CalculateScore(int[] diceValues);
    public abstract void Select();
    public abstract void Unselect();
    public abstract void Freeze();
}
