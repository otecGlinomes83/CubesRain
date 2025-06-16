using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    private int _minDelay = 2;
    private int _maxDelay = 5;

    public MeshRenderer MeshRenderer { get; private set; }
    public bool IsReleasing { get; private set; } = false;

    public float GenerateDelay() => Random.Range(_minDelay, _maxDelay);

    public void SetReleasing(bool newState) =>
        IsReleasing = newState;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }
}