using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YatzyCombinationPresenter
{
    private YatzyCombinationModel yatzyCombinationModel;
    private YatzyCombinationView yatzyCombinationView;

    public YatzyCombinationPresenter(YatzyCombinationModel yatzyCombinationModel, YatzyCombinationView yatzyCombinationView)
    {
        this.yatzyCombinationModel = yatzyCombinationModel;
        this.yatzyCombinationView = yatzyCombinationView;
    }

    public void Initialize()
    {
        ActivateEvents();

        yatzyCombinationView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        yatzyCombinationView.Dispose();
    }

    private void ActivateEvents()
    {
        yatzyCombinationModel.OnSetNumbersCombination += yatzyCombinationView.SetNumbersCombination;
    }

    private void DeactivateEvents()
    {
        yatzyCombinationModel.OnSetNumbersCombination -= yatzyCombinationView.SetNumbersCombination;
    }

    #region Input

    public void SetNumbersCombination(int[] numbers)
    {
        yatzyCombinationModel.SetNumbersCombination(numbers);
    }

    #endregion
}
