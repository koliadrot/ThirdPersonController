using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер камеры игрока для мобильных устройств
/// </summary>
[RequireComponent(typeof(InputMobileManager))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform cinemachineCameraMobileTarget;
    public Transform CinemachineCameraMobileTarget => cinemachineCameraMobileTarget;
    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    private InputMobileManager inputMobile;

    private float topClamp = 70.0f;
    private float bottomClamp = -30.0f;
    private float cameraAngleOverride = 0.0f;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private const float THRESHOLD = 0.01f;

    #if UNITY_IOS || UNITY_ANDROID
    private void Awake()
    {
        cinemachine.Follow = cinemachineCameraMobileTarget;
        inputMobile = GetComponent<InputMobileManager>();
    }

    private void LateUpdate()
    {
        CameraRotation(inputMobile.GetLookPosition());
    }

    private void CameraRotation(Vector2 position)
    {
        // if there is an input and camera position is not fixed
        if (position.sqrMagnitude >= THRESHOLD)
        {
            _cinemachineTargetYaw += position.x * Time.deltaTime;
            _cinemachineTargetPitch += position.y * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

        // Cinemachine will follow this target
        cinemachineCameraMobileTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch + cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        //cinemachine.GetComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = position.y;
        //cinemachine.GetComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = position.x;
    }
    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f)
        {
            lfAngle += 360f;
        }
        if (lfAngle > 360f)
        {
            lfAngle -= 360f;
        }
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
#endif
}
