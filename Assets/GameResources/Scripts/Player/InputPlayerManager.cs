using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Менеджер для входящих значений новой системы ввода
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputPlayerManager : MonoBehaviour
{
	public Vector2 Move { get; set; }
	public bool IsJump { get; set; }
	public bool IsSprint { get; set; }

	public void OnMove(InputValue value)
	{
		Move = value.Get<Vector2>();
	}

	public void OnJump(InputValue value)
	{
		IsJump = value.isPressed;
	}

	public void OnSprint(InputValue value)
	{
		IsSprint = value.isPressed;
	}
}
