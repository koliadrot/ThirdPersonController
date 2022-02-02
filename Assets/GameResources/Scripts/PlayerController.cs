using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер игрока
/// </summary>
public class PlayerController : MonoBehaviour, IStatable
{
    private AbstractPlayerState currentState;

    void IStatable.TransitionToState(AbstractPlayerState state)
    {
        currentState = state;
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
