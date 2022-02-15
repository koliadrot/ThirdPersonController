using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Переключатель синемашин
/// </summary>
public class SwitchCinemachine : MonoBehaviour
{
    [SerializeField]
    private GameObject standaloneCinemachine;
    [SerializeField]
    private GameObject mobileCinemachine;

    private void Awake()
    {
        DisableCinemachines();
#if UNITY_IOS || UNITY_ANDROID
        mobileCinemachine.SetActive(true);
#else
        standaloneCinemachine.SetActive(true);
#endif
    }

    private void DisableCinemachines()
    {
        mobileCinemachine.SetActive(false);
        standaloneCinemachine.SetActive(false);
    }
}
