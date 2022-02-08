using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер состояний игрока
/// </summary>
[RequireComponent(typeof(InputPlayerManager))]
public class PlayerController : MonoBehaviour, IStatable
{
    [SerializeField]
    private BasePlayerState currentState;

    public BasePlayerState CurrentState => currentState;

    private void Start()
    {
        Initialize(CurrentState);
    }

    void IStatable.TransitionToState(BasePlayerState state)
    {
        currentState.Exit();

        currentState = state;
        state.Enter();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    private void Update()
    {
        currentState.HandleInput();
        currentState.LogicUpdate();
    }

    public void Initialize(BasePlayerState state)
    {
        if (state == null)
        {
            Debug.LogError($"Отсутствует стартовый тип состояния {currentState.GetType()}");
            return;
        }
        currentState = state;
        state.Enter();
    }
}
