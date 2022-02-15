using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Абстрактный джостик
/// </summary>
public abstract class UIAbstractJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    protected RectTransform containerRect;
    [SerializeField]
    protected RectTransform handleRect;

    [SerializeField]
    protected float joystickRange = 50f;
    [SerializeField]
    protected float magnitudeMultiplier = 1f;
    [SerializeField]
    protected bool invertXOutputValue;
    [SerializeField]
    protected bool invertYOutputValue;

    protected Vector2 clampedPosition;
    protected Vector2 outputPosition;
    protected float horizontal;
    protected float vertical;

    protected const float SIZE_DELTA = 2.5f;

    private void Start()
    {
        SetupHandle();
    }

    private void SetupHandle()
    {
        if (handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    /// <summary>
    /// Обрабатывает данные при первом нажатии
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    /// <summary>
    /// Обрабатывает данные при непрерывном движении
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out Vector2 position);

        position = ApplySizeDelta(position);

        clampedPosition = ClampValuesToMagnitude(position);

        outputPosition = ApplyInversionFilter(position);

        OutputPointerEventValue(outputPosition * magnitudeMultiplier);

        if (handleRect)
        {
            UpdateHandleRectPosition(clampedPosition * joystickRange);
        }

    }

    /// <summary>
    /// Обрабатывает значения при последнем отжатии
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        OutputPointerEventValue(Vector2.zero);

        if (handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    /// <summary>
    /// Обрабтка конечных координат
    /// </summary>
    /// <param name="pointerPosition"></param>
    public abstract void OutputPointerEventValue(Vector2 pointerPosition);

    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        handleRect.anchoredPosition = newPosition;
    }

    private Vector2 ApplySizeDelta(Vector2 position)
    {
        horizontal = (position.x / containerRect.sizeDelta.x) * SIZE_DELTA;
        vertical = (position.y / containerRect.sizeDelta.y) * SIZE_DELTA;
        return new Vector2(horizontal, vertical);
    }

    private Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    private Vector2 ApplyInversionFilter(Vector2 position)
    {
        if (invertXOutputValue)
        {
            position.x = InvertValue(position.x);
        }

        if (invertYOutputValue)
        {
            position.y = InvertValue(position.y);
        }

        return position;
    }

    private float InvertValue(float value)
    {
        return -value;
    }
}