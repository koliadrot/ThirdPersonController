using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс состояния игрока
/// </summary>
public interface IStatable
{
    /// <summary>
    /// Изменяет состояние
    /// </summary>
    /// <param name="state"></param>
    void TransitionToState(AbstractPlayerState state);
}
