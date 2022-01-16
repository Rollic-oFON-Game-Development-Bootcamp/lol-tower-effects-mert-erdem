using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnScreenJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform joystickTip, joystickBase;

    public Vector2 JoystickDirection;

    // Start is called before the first frame update
    void Start()
    {
        DisableJoystick();
    }

    private void Update()
    {
        //Debug.Log(JoystickDirection);
    }

    private void EnableJoystick()
    {
        joystickBase.gameObject.SetActive(true);
        JoystickDirection = Vector2.zero;
    }

    private void DisableJoystick()
    {
        joystickBase.gameObject.SetActive(false);
        joystickTip.position = joystickBase.position;
        JoystickDirection = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickBase.position = eventData.position;
        EnableJoystick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DisableJoystick();
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystickTip.position = eventData.position;
        var tipOffset = Vector3.ClampMagnitude((joystickTip.position - joystickBase.position), 70f);
        JoystickDirection = tipOffset / 70f;
        joystickTip.position = joystickBase.position + tipOffset;
    }
}
