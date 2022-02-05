using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние "Idle"
/// </summary>
public class IdlePlayerState : AbstractPlayerState
{
    private RunPlayerState runState;
    private JumpPlayerState jumpState;

    protected override void Awake()
    {
        base.Awake();
        ChangeState(this);
        runState = GetComponent<RunPlayerState>();
        jumpState = GetComponent<JumpPlayerState>();
    }

    public override void FixedUpdateState()
    {
        if (GetMovementStatus())
        {
            ChangeState(runState);
        }
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(jumpState);
        }
    }
}