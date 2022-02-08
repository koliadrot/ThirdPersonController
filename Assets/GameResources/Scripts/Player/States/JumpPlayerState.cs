using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние прыжка
/// </summary>
public class JumpPlayerState : GroundedState
{
    [SerializeField]
    private float jumpForce = 5f;

    private int animIDJump;

    private IdlePlayerState idleState;

    private const string ANIMATION_JUMP = "Jump";

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        idleState = GetComponent<IdlePlayerState>();
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
            ChangeState(idleState);
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
