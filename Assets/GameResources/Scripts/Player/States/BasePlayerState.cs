using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс состояния игрока
/// </summary>
public class BasePlayerState
{
    protected IStatable statable;
    protected IMachinable machinable;

    public BasePlayerState(IStatable _statable, IMachinable _machinable)
    {
        statable = _statable;
        machinable = _machinable;
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
    protected virtual void ChangeState(BasePlayerState state)
    {
        if (state == null)
        {
            Debug.LogError("Переход невозможен. Тип состояния null");
            return;
        }
        statable?.TransitionToState(state);
    }
}