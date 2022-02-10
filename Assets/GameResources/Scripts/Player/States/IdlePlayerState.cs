using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние "Idle"
/// </summary>
public class IdlePlayerState : GroundedState
{
    public IdlePlayerState(IStatable _statable, IMachinable _machinable, PlayerController _playerController) : base(_statable, _machinable,_playerController)
    {
        Constructor();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (GetMovementStatus())
        {
            ChangeState(machinable.RunState);
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
#if ENABLE_INPUT_SYSTEM
        if (input.IsJump && GetGroundStatus())
#else
        if (Input.GetKeyDown(KeyCode.Space) && GetGroundStatus())
#endif
        {
            ChangeState(machinable.JumpState);
        }
    }
}