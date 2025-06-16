using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnReadyForRelease;

    private ColorChanger _colorChanger = new ColorChanger();
    private Color _originalColor;

    private int _minDelay = 2;
    private int _maxDelay = 5;

    public MeshRenderer MeshRenderer { get; private set; }
    public bool IsReleasing { get; private set; } = false;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        _originalColor = MeshRenderer.material.color;
    }

    public float GenerateDelay() => UnityEngine.Random.Range(_minDelay, _maxDelay);

    public void SetReleasing(bool newState) =>
        IsReleasing = newState;

    public void OnCubeDetected()
    {
        if (IsReleasing)
            return;

        IsReleasing = true;

        _colorChanger.SetRandomColor(MeshRenderer);

        StartCoroutine(DelayedRelease(GenerateDelay()));
    }

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSeconds(delay);

        IsReleasing = false;

        _colorChanger.SetColor(MeshRenderer, _originalColor);

        OnReadyForRelease?.Invoke(this);
    }
}