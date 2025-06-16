using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HitDetector : MonoBehaviour
{
    private Collider _triggerZone;

    private void Awake()
    {
        _triggerZone = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube) == false)
            return;

        cube.OnCubeDetected();
    }
}
