using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewContainer : MonoBehaviour
{
    private Dictionary<Type, View> viewsWithoutID = new Dictionary<Type, View>();
    private Dictionary<(Type, string), View> viewsWithID = new Dictionary<(Type, string), View>();

    public void Initialize()
    {
        var views = GetComponentsInChildren<View>();

        foreach (var view in views)
        {
            RegisterView(view.GetType(), view);
        }
    }

    public void RegisterView(Type type, View view)
    {
        if(view is IIdentify identify)
        {
            var key = (type, identify.GetID());
            if(!viewsWithID.ContainsKey(key))
            {
                viewsWithID.Add(key, view);
            }
            else
            {
                Debug.LogError("View c ����� " + type + " � ��������������� " + key + " ��� ��� ���������������");
            }
        }
        else
        {
            if (!viewsWithoutID.ContainsKey(type))
            {
                viewsWithoutID.Add(type, view);
            }
            else
            {
                Debug.LogError("View c ����� " + type + " � ��������������� " + type + " ��� ��� ���������������");
            }
        }

        if (!viewsWithoutID.ContainsKey(type))
        {
            viewsWithoutID.Add(type, view);
        }
    }

    public T GetView<T>() where T : View
    {
        var type = typeof(T);

        if(viewsWithoutID.TryGetValue(type, out View view))
        {
            return (T)view;
        }

        Debug.Log("View ���� " + type + " �� ��� ������");
        return null;
    }

    public T GetView<T>(string ID) where T : View
    {
        var type = (typeof(T), ID);

        if (viewsWithID.TryGetValue(type, out View view))
        {
            return (T)view;
        }

        Debug.Log("View ���� " + type + " �� ��� ������");
        return null;
    }
}

public interface IIdentify
{
    string GetID();
}
