using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый класс состояния игрока
/// </summary>
public class BaseState
{
    protected IStatable statble;
    protected StateMachine stateMachine;

    public BaseState(IStatable _statable,StateMachine _stateMachine)
    {
        statble = _statable;
        stateMachine = _stateMachine;
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
    protected virtual void Constructor()
    {

    }
    protected virtual void ChangeState(BaseState state)
    {
        if (state == null)
        {
            Debug.LogError("Переход невозможен. Тип состояния null");
            return;
        }
        stateMachine?.TransitionToState(state);
    }
}