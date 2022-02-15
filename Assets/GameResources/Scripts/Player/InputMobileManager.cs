using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для работы с входными данными у мобильных устройств
/// </summary>
public class InputMobileManager : MonoBehaviour
{
    [SerializeField]
    private UIMoveJostick moveJostick;
    [SerializeField]
    private UILookJoystick lookJoystick;

    private void Awake()
    {
        if (moveJostick == null)
        {
            moveJostick = FindObjectOfType<UIMoveJostick>();
        }
        if (lookJoystick == null)
        {
            lookJoystick = FindObjectOfType<UILookJoystick>();
        }
    }

    /// <summary>
    /// Возвращает координаты джостика движения
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMovePosition() => moveJostick.MovePosition;

    /// <summary>
    /// Возвращает координаты джостика камеры
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLookPosition() => lookJoystick.LookPosition;
}
