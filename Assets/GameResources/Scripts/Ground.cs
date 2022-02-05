using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сущность земли
/// </summary>
public class Ground : MonoBehaviour
{
    [SerializeField, Min(1)]
    private float sizeX = 1f;
    [SerializeField, Min(1)]
    private float sizeZ = 1f;

    private void Awake()
    {
        transform.localScale = new Vector3(sizeX, 1f, sizeZ);
    }
}
