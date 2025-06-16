using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    public Rigidbody _rigidbody;

    private ColorChanger _colorChanger = new ColorChanger();

    private Color _defaultColor;

    private bool _isReleasing = false;

    private int _minDelay = 2;
    private int _maxDelay = 5;

    public event Action<Cube> ReadyForRelease;

    public MeshRenderer MeshRenderer { get; private set; }

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _defaultColor = MeshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Platform>(out _) == false)
            return;

        if (_isReleasing)
            return;

        _isReleasing = true;

        _colorChanger.SetRandomColor(MeshRenderer);

        StartCoroutine(DelayedRelease(GenerateDelay()));
    }

    public void SetDefault()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _colorChanger.SetColor(MeshRenderer, _defaultColor);
    }

    public float GenerateDelay() =>
        UnityEngine.Random.Range(_minDelay, _maxDelay);

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSeconds(delay);

        ReadyForRelease?.Invoke(this);

        _isReleasing = false;
    }
}