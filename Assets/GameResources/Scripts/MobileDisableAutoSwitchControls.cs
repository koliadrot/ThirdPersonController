using UnityEngine;

/// <summary>
/// ����������� ��������� ������������ �� ���������
/// </summary>
public class MobileDisableAutoSwitchControls : MonoBehaviour
{
    private void Start()
    {
#if (UNITY_IOS || UNITY_ANDROID)
        DisableAutoSwitchControls(true);
#else
        DisableAutoSwitchControls(false);
#endif
    }

    private void DisableAutoSwitchControls(bool state)
    {
        gameObject.SetActive(state);
    }

}
