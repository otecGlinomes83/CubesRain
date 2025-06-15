using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Collider TriggerZone;

    public event Action<Cube> CubeDetected;

    private void OnTriggerEnter(Collider other)
    {
        Cube cube;

        if (other.TryGetComponent<Cube>(out cube) == false)
            return;

        CubeDetected?.Invoke(cube);
    }
}
