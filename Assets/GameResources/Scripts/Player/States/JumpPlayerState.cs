using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние прыжка
/// </summary>
public class JumpPlayerState : BasePlayerState
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

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        JumpAndGravity();
    }

    private void JumpAndGravity()
    {
        if (GetGroundStatus())
        {
            animator.SetBool(animIDJump, true);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (!GetGroundStatus())
        {
            ChangeState(idleState,delegate
            {
                animator.SetBool(animIDJump, false);
            });
        }
    }
}
