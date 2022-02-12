using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс состояния "jump"
/// </summary>
public class JumpState : GroundedState
{
    private float jumpForce = 5f;

    private int animIDJump;

    private const string ANIMATION_JUMP = "Jump";

    public JumpState(IStatable _statable, StateMachine _stateMachine, PlayerController _playerController) : base(_statable, _stateMachine, _playerController)
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
            ChangeState(statble.IdleState);
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
