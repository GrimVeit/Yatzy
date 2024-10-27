using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyCombinationModel
{
    public event Action<int[]> OnSetNumbersCombination;

    public void SetNumbersCombination(int[] numbers)
    {
        OnSetNumbersCombination?.Invoke(numbers);
    }
}
