using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние прыжка
/// </summary>
public class JumpPlayerState : GroundedState
{
    private float jumpForce = 5f;

    private int animIDJump;

    private const string ANIMATION_JUMP = "Jump";

    public JumpPlayerState(IStatable _statable, IMachinable _machinable, PlayerController _playerController) : base(_statable, _machinable, _playerController)
    {
        Constructor();
    }

    protected override void Constructor()
    {
        base.Constructor();
        animIDJump = Animator.StringToHash(ANIMATION_JUMP);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        GravityFalling();
    }

    private void GravityFalling()
    {
        if (!GetGroundStatus())
        {
            ChangeState(machinable.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(animIDJump, false);
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    private void Jump()
    {
        if (GetGroundStatus())
        {
            animator.SetBool(animIDJump, true);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
