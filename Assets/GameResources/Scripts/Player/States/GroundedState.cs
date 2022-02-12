using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс состояния "ground"
/// </summary>
public class GroundedState : BaseState
{
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected Animator animator;
    protected InputPlayerManager input;
    protected Transform target;
    protected PlayerController playerController;

    private int animIDFreeFall;
    private int animIDGrounded;
    private float groundHeight;
    private LayerMask layer;
    private bool isFalling;

    private const string ANIMATION_FREE_FALL = "FreeFall";
    private const string ANIMATION_IDLE = "Grounded";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND = "Ground";
    private const float OFFSET_GROUND = 1.05f;

    public GroundedState(IStatable _statable, StateMachine _stateMachine, PlayerController _playerController) : base(_statable, _stateMachine)
    {
        playerController = _playerController;
        Constructor();
    }

    protected override void Constructor()
    {
        base.Constructor();
        rigidBody = playerController.RigidBody;
        capsuleCollider = playerController.СapsuleCollider;
        animator = playerController.Animator;
        input = playerController.InputPlayer;
        target = playerController.transform;
        layer.value = LayerMask.GetMask(GROUND);
        animIDFreeFall = Animator.StringToHash(ANIMATION_FREE_FALL);
        animIDGrounded = Animator.StringToHash(ANIMATION_IDLE);
        groundHeight = (capsuleCollider.height / 2f- capsuleCollider.radius)*OFFSET_GROUND - capsuleCollider.center.y;
        SetGround(GetGroundStatus());
    }

    public override void HandleInput()
    {
        base.HandleInput();
        CheckFalling();
    }
    private void CheckFalling()
    {
        if (isFalling == GetGroundStatus())
        {
            return;
        }
        SetGround(GetGroundStatus());
    }
    private void SetGround(bool status)
    {
        isFalling = status;
        animator.SetBool(animIDGrounded, status);
        animator.SetBool(animIDFreeFall, !status);
    }

    protected bool GetGroundStatus()
    {
        return Physics.CheckSphere(target.position+Vector3.down*groundHeight, capsuleCollider.radius, layer);
    }
    protected bool GetMovementStatus()
    {
        return GetHorizontalPosition() != 0f || GetVerticalPosition() != 0f;
    }
    protected virtual float GetHorizontalPosition()
    {
#if ENABLE_INPUT_SYSTEM
       return input.Move.x;
#else
       return Input.GetAxis(HORIZONTAL);
#endif
    }
    protected virtual float GetVerticalPosition()
    {
#if ENABLE_INPUT_SYSTEM
        return input.Move.y;
#else
        return Input.GetAxis(VERTICAL);
#endif
    }
}
