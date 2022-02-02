using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс состояния игрока
/// </summary>
[RequireComponent(typeof(PlayerController))]
public abstract class AbstractPlayerState:MonoBehaviour
{
    [SerializeField]
    private bool isDefaultState;

    protected IStatable statable = default;

    protected virtual void Awake()
    {
        statable = GetComponent<PlayerController>();
        if (isDefaultState)
        {
            statable?.TransitionToState(this);
        }
    }

    /// <summary>
    /// Вызов в каждом кадре
    /// </summary>
    public abstract void UpdateState();
}