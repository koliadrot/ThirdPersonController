using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер состояний игрока
/// </summary>
public class PlayerController : MonoBehaviour, IStatable
{
    private BasePlayerState currentState;

    void IStatable.TransitionToState(BasePlayerState state)
    {
        currentState = state;
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    private void Update()
    {
        currentState.HandleInput();
    }
}
