using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyCombinationModel
{
    public event Action<int> OnChooseFreeseCombination;
    public event Action OnSubmitFreeseCombination;

    public event Action<int[]> OnSetNumbersCombination;

    private int currentChooseIndexForFreeseCombination;

    public void SetNumbersCombination(int[] numbers)
    {
        OnSetNumbersCombination?.Invoke(numbers);
    }

    public void ChooseFreezeCombination(int index)
    {
        currentChooseIndexForFreeseCombination = index;
    }

    public void SubmitFreezeCombination()
    {
        OnSubmitFreeseCombination?.Invoke();
    }
}
