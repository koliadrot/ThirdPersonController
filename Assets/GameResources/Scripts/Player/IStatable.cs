using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс состояния игрока
/// </summary>
public interface IStatable
{
    BasePlayerState CurrentState { get; }

    /// <summary>
    /// Изменяет состояние
    /// </summary>
    /// <param name="state"></param>
    void TransitionToState(BasePlayerState state);

    void Initialize(BasePlayerState state);
}
