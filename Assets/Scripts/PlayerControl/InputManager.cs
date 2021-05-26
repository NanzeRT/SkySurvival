using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public delegate void Vector2Handler(Vector2 v);
    static Vector2Handler  OnMouseMovement = null;
    private bool CursorLocked = false;

    public static void SubscribeOnMouseMovement(Vector2Handler method)
    {
        OnMouseMovement += method;
    }

    void Update()
    {
        
    }

    void HandleMouseMovement()
    {

    }

    void HandleTouchMovement()
    {

    }
}
