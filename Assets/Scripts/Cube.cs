using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    private int _minDelay = 2;
    private int _maxDelay = 5;

    public MeshRenderer MeshRenderer { get; private set; }

    public float GenerateDelay() => Random.Range(_minDelay, _maxDelay);

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }
}