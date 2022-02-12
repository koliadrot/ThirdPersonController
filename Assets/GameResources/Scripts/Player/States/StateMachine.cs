using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для взаимодействия с состояниями
/// </summary>
public class StateMachine
{
    public BaseState CurrentState { get; private set; }

    public void TransitionToState(BaseState state)
    {
        CurrentState.Exit();

        CurrentState = state;
        state.Enter();
    }

    public void Initialize(BaseState startingState)
    {
        if (startingState == null)
        {
            Debug.LogError($"Отсутствует стартовый тип состояния {startingState.GetType()}");
            return;
        }
        CurrentState = startingState;
        startingState.Enter();
    }
}
