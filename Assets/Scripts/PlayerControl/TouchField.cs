using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float sensitivity = 1f;
    public Vector2 deltaPos;
    public Vector2 touchPos;
    public int touchId;
    public bool pressed = false;

    void Update()
    {
        if (!pressed) return;

        if (touchId >= 0 && touchId < Input.touches.Length)
        {
            deltaPos = (Input.touches[touchId].position - touchPos) * sensitivity;
            touchPos = Input.touches[touchId].position;
        }
        else
        {
            deltaPos = ((Vector2) Input.mousePosition - touchPos) * sensitivity;
            touchPos = (Vector2) Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressed) return;
        pressed = true;
        touchId = eventData.pointerId;
        touchPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != touchId)
            return;
        pressed = false;
        deltaPos = Vector2.zero;
    }
}
