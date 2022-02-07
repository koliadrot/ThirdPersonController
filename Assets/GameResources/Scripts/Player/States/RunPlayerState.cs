using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние бега
/// </summary>
public class RunPlayerState : BasePlayerState
{
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float sprintSpeed = 20f;
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
    private float animationBlend;

    private IEnumerator animationCoroutine;

    private Vector3 targetDirection;
    private Vector3 inputDirection;

    private IdlePlayerState idleState;
    private JumpPlayerState jumpState;

    private int animIDSpeed;

    private const string ANIMATION_SPEED = "Speed";
    private const float DURATION = 70f;

    protected override void Awake()
    {
        base.Awake();
        idleState = GetComponent<IdlePlayerState>();
        jumpState = GetComponent<JumpPlayerState>();
        animIDSpeed = Animator.StringToHash(ANIMATION_SPEED);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Move();
        Animate();
    }

    private void Move()
    {
        targetSpeed = sprint ? sprintSpeed : moveSpeed;

        currentHorizontalSpeed = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z).magnitude;

        speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
        speed = Mathf.Round(speed * 1000f) / 1000f;

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        inputDirection = new Vector3(GetHorizontalPosition(), 0.0f, GetVerticalPosition()).normalized;

        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
        
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        rigidBody.MovePosition(transform.position + targetDirection.normalized * (speed * Time.deltaTime));
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!GetMovementStatus())
        {
            ChangeState(idleState,delegate
            {
                sprint = false;
                OnSmoothDampingAnimate();
            });
        }
#if ENABLE_INPUT_SYSTEM
        if (input.IsJump && GetGroundStatus())
#else
        if (Input.GetKeyDown(KeyCode.Space) && GetGroundStatus())
#endif
        {
            ChangeState(jumpState);
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

    private void Animate()
    {
        if (GetGroundStatus())
        {
            animator.SetFloat(animIDSpeed, animationBlend);
        }
    }

    private void OnSmoothDampingAnimate()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = SmoothDampingAnimate();
        StartCoroutine(animationCoroutine);
    }

    private IEnumerator SmoothDampingAnimate()
    {
        while (animationBlend > 0f)
        {
            animationBlend -= Time.deltaTime*DURATION;
            animator.SetFloat(animIDSpeed, animationBlend);
            yield return null;
            if (GetMovementStatus())
            {
                break;
            }
        }
        animationCoroutine = null;
    }
}
