using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс состояния игрока
/// </summary>
[RequireComponent(typeof(PlayerController), typeof(Rigidbody), typeof(CapsuleCollider)), RequireComponent(typeof(Animator))]
public class BasePlayerState : MonoBehaviour
{
    protected IStatable statable = default;
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;
    protected Animator animator;

    private int animIDFreeFall;
    private int animIDGrounded;
    private float lenthRay;
    private RaycastHit raycastHit;
    private LayerMask layer;

    private const string ANIMATION_FREE_FALL = "FreeFall";
    private const string ANIMATION_IDLE = "Grounded";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND = "Ground";
    private const float OFFSET_GROUND = 1.05f;

    protected virtual void Awake()
    {
        layer.value = LayerMask.GetMask(GROUND);
        statable = GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        animIDFreeFall = Animator.StringToHash(ANIMATION_FREE_FALL);
        animIDGrounded = Animator.StringToHash(ANIMATION_IDLE);
        lenthRay = (capsuleCollider.height / 2f * OFFSET_GROUND) - capsuleCollider.center.y;
    }

    protected bool GetGroundStatus()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, lenthRay,layer))
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
        return Input.GetAxis(HORIZONTAL);
    }
    protected virtual float GetVerticalPosition()
    {
        return Input.GetAxis(VERTICAL);
    }

    protected virtual void ChangeState(BasePlayerState state,Action action=null)
    {
        if (state == null)
        {
            Debug.LogError("Отсутствует тип состояния");
            return;
        }
        statable?.TransitionToState(state);
        action?.Invoke();
    }

    /// <summary>
    /// Обработка вычислений физики
    /// </summary>
    public virtual void FixedUpdateState()
    {
        animator.SetBool(animIDFreeFall, !GetGroundStatus());
    }

    /// <summary>
    /// Обработка входящих значений
    /// </summary>
    public virtual void HandleInput()
    {
        animator.SetBool(animIDGrounded, GetGroundStatus());
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * lenthRay, Color.red);
    }
#endif
}