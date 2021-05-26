using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceToggle : MonoBehaviour
{
    [SerializeField]
    bool isEnabled = true;
    public bool IsEnabled
    {
        get => isEnabled;
        set { isEnabled = value; SetEnabled(); }
    }

    public void Toggle()
    {
        IsEnabled = !IsEnabled;
    }

    void SetEnabled()
    {
        gameObject.SetActive(isEnabled);
    }

    void Start()
    {
        SetEnabled();
    }
}
