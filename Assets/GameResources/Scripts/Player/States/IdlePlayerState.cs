using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Состояние "Idle"
/// </summary>
public class IdlePlayerState : BasePlayerState
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
        base.FixedUpdateState();
        if (GetMovementStatus())
        {
            ChangeState(runState);
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.Space) && GetGroundStatus())
        {
            ChangeState(jumpState);
        }
    }
}