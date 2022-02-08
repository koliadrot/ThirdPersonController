using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController), typeof(Rigidbody), typeof(CapsuleCollider)), RequireComponent(typeof(Animator))]
public class GroundedState : BasePlayerState
{
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected Animator animator;
    protected InputPlayerManager input;

    private int animIDFreeFall;
    private int animIDGrounded;
    private float lenthRay;
    private LayerMask layer;
    private bool isFalling;

    private const string ANIMATION_FREE_FALL = "FreeFall";
    private const string ANIMATION_IDLE = "Grounded";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND = "Ground";
    private const float OFFSET_GROUND = 1.05f;

    protected override void Awake()
    {
        base.Awake();
        layer.value = LayerMask.GetMask(GROUND);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        input = GetComponent<InputPlayerManager>();
        animIDFreeFall = Animator.StringToHash(ANIMATION_FREE_FALL);
        animIDGrounded = Animator.StringToHash(ANIMATION_IDLE);
        lenthRay = (capsuleCollider.height / 2f * OFFSET_GROUND) - capsuleCollider.center.y;
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
        if (Physics.Raycast(transform.position, Vector3.down, lenthRay, layer))
        {
            return true;
        }
        return false;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * lenthRay, Color.red);
    }
#endif
}
