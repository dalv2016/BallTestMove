using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _screenPoint;
    public static Action<Vector3, Vector3> _speedCalc;
    public static Action apply;

    public static Action _normalVect3;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
      
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 _cursorPoint = new Vector3(eventData.position.x, eventData.position.y, _screenPoint.z);
        Vector3 _cursorPosition = Camera.main.ScreenToWorldPoint(_cursorPoint);
        _speedCalc?.Invoke(transform.position, _cursorPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        apply?.Invoke();
        _normalVect3?.Invoke();
    }

}
