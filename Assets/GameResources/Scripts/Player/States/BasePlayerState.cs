using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс состояния игрока
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class BasePlayerState : MonoBehaviour
{
    protected IStatable statable = default;

    protected virtual void Awake()
    {
        statable = GetComponent<PlayerController>();
    }

    public virtual void Enter()
    {

    }
    public virtual void HandleInput()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit()
    {

    }

    protected virtual void ChangeState(BasePlayerState state)
    {
        if (state == null)
        {
            Debug.LogError("Тип состояния null");
            return;
        }
        statable?.TransitionToState(state);
    }
}