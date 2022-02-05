using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс состояния игрока
/// </summary>
[RequireComponent(typeof(PlayerController), typeof(Rigidbody),typeof(CapsuleCollider))]
public abstract class AbstractPlayerState : MonoBehaviour
{
    protected IStatable statable = default;
    protected Rigidbody rigidBody;
    protected CapsuleCollider capsuleCollider;

    private float lenthRay;
    private RaycastHit raycastHit;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const float OFFSET_GROUND = 1.1f;



    protected virtual void Awake()
    {
        statable = GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        lenthRay = capsuleCollider.height / 2f * OFFSET_GROUND;
    }

    protected bool GetGroundStatus()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit,lenthRay))
        {
            if(raycastHit.collider.TryGetComponent(out Ground ground))
            {
                return true;
            }
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

    protected virtual void ChangeState(AbstractPlayerState state)
    {
        statable?.TransitionToState(state);
    }

    /// <summary>
    /// Обработка вычислений физики
    /// </summary>
    public abstract void FixedUpdateState();

    /// <summary>
    /// Обработка входящих значений
    /// </summary>
    public abstract void HandleInput();
}