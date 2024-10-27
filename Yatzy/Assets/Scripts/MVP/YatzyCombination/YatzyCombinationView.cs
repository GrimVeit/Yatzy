using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YatzyCombinationView : View
{
    [SerializeField] private List<YatzyCombination> yatzyCombinations = new List<YatzyCombination>();

    private YatzyCombination currentChooseCombination;

    public void Initialize()
    {
        for (int i = 0; i < yatzyCombinations.Count; i++)
        {
            yatzyCombinations[i].OnChooseCombination += HandleChooseCombination;
            yatzyCombinations[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < yatzyCombinations.Count; i++)
        {
            yatzyCombinations[i].OnChooseCombination -= HandleChooseCombination;
            yatzyCombinations[i].Dispose();
        }
    }

    public void SetNumbersCombination(int[] numbers)
    {
        for (int i = 0; i < yatzyCombinations.Count; i++)
        {
            yatzyCombinations[i].CalculateScore(numbers);
        }
    }

    private void HandleChooseCombination(YatzyCombination yatzyCombination)
    {
        Debug.Log(yatzyCombination.GetType());

        if(currentChooseCombination != null && currentChooseCombination == yatzyCombination)
        {
            currentChooseCombination.Unselect();
            currentChooseCombination = null;
        }
        else
        {
            currentChooseCombination?.Unselect();

            currentChooseCombination = yatzyCombination;
            currentChooseCombination.Select();
        }
    }
}
