using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class YatzyCombination : MonoBehaviour
{
    public Action<YatzyCombination> OnChooseCombination;

    public virtual void Initialize() { }
    public virtual void Dispose() { }
    public abstract void CalculateScore(int[] diceValues);
    public abstract void Select();
    public abstract void Unselect();
}
