using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Менеджер для входящих значений новой системы ввода
/// </summary>
#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class InputPlayerManager : MonoBehaviour
{
	public Vector2 Move { get; set; }
	public bool IsJump { get; set; }
	public bool IsSprint { get; set; }

	#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM
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
	#endif
}
