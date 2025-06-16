using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class HitDetector : MonoBehaviour
{
    private Collider _triggerZone;

    public event Action<Cube> CubeDetected;

    private void Awake()
    {
        _triggerZone = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cube cube;

        if (other.TryGetComponent<Cube>(out cube) == false)
            return;

        CubeDetected?.Invoke(cube);
    }
}
