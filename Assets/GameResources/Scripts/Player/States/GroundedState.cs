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
    protected InputPlayerManager newInput;
    protected InputMobileManager mobileInput;
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
        newInput = playerController.InputPlayer;
        mobileInput = playerController.InputMobilePlayer;
        target = playerController.transform;
        layer.value = LayerMask.GetMask(GROUND);
        animIDFreeFall = Animator.StringToHash(ANIMATION_FREE_FALL);
        animIDGrounded = Animator.StringToHash(ANIMATION_IDLE);
        groundHeight = (capsuleCollider.height / 2f - capsuleCollider.radius) * OFFSET_GROUND - capsuleCollider.center.y;
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
        return Physics.CheckSphere(target.position + Vector3.down * groundHeight, capsuleCollider.radius, layer);
    }
    protected bool GetMovementStatus()
    {
        return GetPosition().magnitude != 0f;
    }
    protected virtual Vector2 GetPosition()
    {
#if UNITY_IOS || UNITY_ANDROID
        return mobileInput.GetMovePosition();
#elif ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM
        return newInput.Move;
#else
        return new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
#endif
    }
}
