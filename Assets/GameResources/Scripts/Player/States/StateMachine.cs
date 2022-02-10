using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine:IStatable
{
    public BasePlayerState CurrentState { get; private set; }

    public void TransitionToState(BasePlayerState state)
    {
        CurrentState.Exit();

        CurrentState = state;
        state.Enter();
    }

    public void Initialize(BasePlayerState startingState)
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
