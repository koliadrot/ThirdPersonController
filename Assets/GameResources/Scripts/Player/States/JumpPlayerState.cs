using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerState : AbstractPlayerState
{
    [SerializeField]
    private float jumpForce = 3f;

    private IdlePlayerState idleState;

    protected override void Awake()
    {
        base.Awake();
        idleState = GetComponent<IdlePlayerState>();
    }

    public override void FixedUpdateState()
    {
        JumpAndGravity();
    }

    public override void HandleInput()
    {
        if (!GetGroundStatus())
        {
            ChangeState(idleState);
        }
    }

    private void JumpAndGravity()
    {
        if (GetGroundStatus())
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
