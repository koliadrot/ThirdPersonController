using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер состояний игрока
/// </summary>
public class PlayerController : MonoBehaviour, IStatable
{
    private AbstractPlayerState currentState;

    void IStatable.TransitionToState(AbstractPlayerState state)
    {
        if (state == null)
        {
            Debug.LogError("Отсутствует тип состояния");
            return;
        }
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
