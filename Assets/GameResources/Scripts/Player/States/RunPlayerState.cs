using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние бега
/// </summary>
public class RunPlayerState : AbstractPlayerState
{
    [SerializeField]
    private float moveSpeed = 20f;
    [SerializeField]
    private float sprintSpeed = 50f;
    [SerializeField]
    private float speedChangeRate = 10.0f;
    [SerializeField, Range(0.0f, 0.3f)]
    private float rotationSmoothTime = 0.12f;

    private bool sprint;

    private float speed;
    private float targetRotation;
    private float rotationVelocity;
    private float targetSpeed;
    private float currentHorizontalSpeed;
    private float rotation;

    private Vector3 targetDirection;
    private Vector3 inputDirection;

    private IdlePlayerState idleState;
    private JumpPlayerState jumpState;

    protected override void Awake()
    {
        base.Awake();
        idleState = GetComponent<IdlePlayerState>();
        jumpState = GetComponent<JumpPlayerState>();
    }

    public override void FixedUpdateState()
    {
        Move();
    }

    private void Move()
    {
        targetSpeed = sprint ? sprintSpeed : moveSpeed;

        currentHorizontalSpeed = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z).magnitude;

        speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
        speed = Mathf.Round(speed * 1000f) / 1000f;

        inputDirection = new Vector3(GetHorizontalPosition(), 0.0f, GetVerticalPosition()).normalized;

        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        rigidBody.MovePosition(transform.position + targetDirection.normalized * (speed * Time.deltaTime));
    }

    public override void HandleInput()
    {
        if (!GetMovementStatus())
        {
            sprint = false;
            ChangeState(idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(jumpState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = !sprint;
        }
    }
}
