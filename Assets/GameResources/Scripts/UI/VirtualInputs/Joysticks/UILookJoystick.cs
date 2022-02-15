using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Джостик поворота камеры
/// </summary>
public class UILookJoystick : UIAbstractJoystick
{
    public Vector2 LookPosition { get; private set; }

    public override void OutputPointerEventValue(Vector2 pointerPosition)
    {
        LookPosition = pointerPosition;
    }
}
