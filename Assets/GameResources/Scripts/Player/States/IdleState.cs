using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс состояния "idle"
/// </summary>
public class IdleState : GroundedState
{
    public IdleState(IStatable _statable, StateMachine _stateMachine, PlayerController _playerController) : base(_statable, _stateMachine, _playerController)
    {
        Constructor();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (GetMovementStatus())
        {
            ChangeState(statble.RunState);
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM
        if (newInput.IsJump && GetGroundStatus())
#else
        if (Input.GetKeyDown(KeyCode.Space) && GetGroundStatus())
#endif
        {
            ChangeState(statble.JumpState);
        }
    }
}