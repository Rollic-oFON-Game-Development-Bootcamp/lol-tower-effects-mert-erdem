using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private OnScreenJoystick joystick;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystick.transform.position = eventData.position;
        joystick.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
