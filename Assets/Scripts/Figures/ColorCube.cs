using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class ColorCube : FallingObject
{
    public event Action<ColorCube> ReadyForRelease;

    private Color _defaultColor;

    protected override void Awake()
    {
        base.Awake();
        _defaultColor = _meshRenderer.material.color;
    }

    public override void SetDefault()
    {
        base.SetDefault();
        _colorChanger.SetColor(_meshRenderer, _defaultColor);
    }

    protected override IEnumerator DelayedRelease(float delay)
    {
        _isReleasing = true;
        _colorChanger.SetRandomColor(_meshRenderer);
        yield return new WaitForSeconds(delay);

        ReadyForRelease?.Invoke(this);
        _isReleasing = false;
    }
}