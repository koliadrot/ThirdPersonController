using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Класс состояния "run"
/// </summary>
public class RunState : GroundedState
{
    private float sprintSpeed = 20f;
    private float animationBlend;

    private bool sprint;

    private CancellationTokenSource cancellationToken;
    private Task taskDamping;

    private float moveSpeed = 7f;
    private float speedChangeRate = 10.0f;
    private float rotationSmoothTime = 0.12f;
    private float speed;
    private float targetRotation;
    private float rotationVelocity;
    private float targetSpeed;
    private float currentHorizontalSpeed;
    private float rotation;

    private Vector3 targetDirection;
    private Vector3 inputDirection;
    private Camera targetCamera;

    private int animIDSpeed;

    private const string ANIMATION_SPEED = "Speed";
    private const float DURATION = 70f;
    private const int ROUND = 1000;


    public RunState(IStatable _statable, StateMachine _stateMachine, PlayerController _playerController,Camera _targetCamera) : base(_statable, _stateMachine, _playerController)
    {
        targetCamera = _targetCamera;
        Constructor();
    }

    protected override void Constructor()
    {
        base.Constructor();
        animIDSpeed = Animator.StringToHash(ANIMATION_SPEED);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Move();
    }
    public override void Exit()
    {
        base.Exit();
        OnStopSmoothDampingAnimate();
        OnStartSmoothDampingAnimate();
    }
    public override void Enter()
    {
        base.Enter();
        OnStopSmoothDampingAnimate();
        if (animationBlend <= 0f)
        {
            sprint = false;
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!GetMovementStatus())
        {
            ChangeState(statble.IdleState);
        }
#if ENABLE_INPUT_SYSTEM
        if (input.IsJump && GetGroundStatus())
#else
        if (Input.GetKeyDown(KeyCode.Space) && GetGroundStatus())
#endif
        {
            ChangeState(statble.JumpState);
        }
#if ENABLE_INPUT_SYSTEM
        if (input.IsSprint)
#else
        if (Input.GetKeyDown(KeyCode.LeftShift))
#endif
        {
            sprint = !sprint;
        }
    }
    private void Move()
    {
        targetSpeed = sprint ? sprintSpeed : moveSpeed;
        currentHorizontalSpeed = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z).magnitude;

        speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
        speed = Mathf.Round(speed * ROUND) / ROUND;

        inputDirection = new Vector3(GetPosition().x, 0.0f, GetPosition().y).normalized;

        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + targetCamera.transform.eulerAngles.y;
        rotation = Mathf.SmoothDampAngle(target.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

        target.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        rigidBody.MovePosition(target.position + targetDirection.normalized * (speed * Time.deltaTime));
        Animate();
    }

    private void Animate()
    {
        if (GetGroundStatus())
        {
            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            animator.SetFloat(animIDSpeed, animationBlend);
        }
    }

    private void OnStartSmoothDampingAnimate( )
    {
        taskDamping = SmoothDampingAnimate(cancellationToken.Token);
    }
    private void OnStopSmoothDampingAnimate()
    {
        if (taskDamping != null && (!taskDamping.IsCompleted || !taskDamping.IsCanceled))
        {
            cancellationToken.Cancel();
        }
        cancellationToken = new CancellationTokenSource();
    }

    private async Task SmoothDampingAnimate(CancellationToken tokenSource)
    {
        while (true)
        {
            animationBlend -= Time.deltaTime*DURATION;
            animator.SetFloat(animIDSpeed, animationBlend);

            if (tokenSource.IsCancellationRequested || animationBlend <= 0f)
            {
                break;
            }
            await Task.Delay(Mathf.CeilToInt(Time.deltaTime * ROUND));
        }
    }
}
