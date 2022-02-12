using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Список рабочих состояний
/// </summary>
public interface IStatable
{
    GroundedState GroundedState { get; }
    RunState RunState { get; }
    IdleState IdleState { get; }
    JumpState JumpState { get; }
}
