using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialView : View
{
    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();

    public void ActivateTutorial(string ID)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].GetID() == ID)
            {
                tutorials[i].Activate();
                return;
            }
        }

        Debug.LogWarning("Tutorial with id - " + ID + " not found");
    }

    public void DeactivateTutorial(string ID)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].GetID() == ID)
            {
                tutorials[i].Deactivate();
                return;
            }
        }

        Debug.LogWarning("Tutorial with id - " + ID + " not found");
    }

    public void AllDeactivates()
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].IsActive())
            {
                tutorials[i].Deactivate();
            }
        }
    }
}
