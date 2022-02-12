using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер состояний игрока
/// </summary>
[RequireComponent(typeof(InputPlayerManager))]
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider)), RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour,IStatable
{
    public GroundedState GroundedState { get; private set; }
    public RunState RunState { get; private set; }
    public IdleState IdleState { get; private set; }
    public JumpState JumpState { get; private set; }

    public Rigidbody RigidBody { get; private set; }
    public CapsuleCollider СapsuleCollider { get; private set; }
    public Animator Animator { get; private set; }
    public InputPlayerManager InputPlayer { get; private set; }

    private StateMachine stateMachine;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        СapsuleCollider = GetComponent<CapsuleCollider>();
        Animator = GetComponent<Animator>();
        InputPlayer = GetComponent<InputPlayerManager>();
        stateMachine = new StateMachine();
        GroundedState = new GroundedState(this,stateMachine,this);
        IdleState = new IdleState(this, stateMachine, this);
        JumpState = new JumpState(this, stateMachine, this);
        RunState = new RunState(this, stateMachine, this);

        stateMachine.Initialize(IdleState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void Update()
    {
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
    }

    ///NOTE:Для отрисовки шара надо поменять модификатор доступа GroundedState.groundHeight
    //#if UNITY_EDITOR
    //    private void OnDrawGizmos()
    //    {
    //        if (GroundedState != null)
    //        {
    //            Gizmos.DrawSphere(transform.position + Vector3.down *GroundedState.groundHeight, СapsuleCollider.radius);
    //        }
    //    }
    //#endif
}
