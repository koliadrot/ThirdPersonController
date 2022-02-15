using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Джостик движения
/// </summary>
public class UIMoveJostick : UIAbstractJoystick
{
    public Vector2 MovePosition { get; private set; }

    public override void OutputPointerEventValue(Vector2 pointerPosition)
    {
        MovePosition = pointerPosition;
    }
}
