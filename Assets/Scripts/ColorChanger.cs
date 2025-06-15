using UnityEngine;

public class ColorChanger
{
    private float _maxColorValue = 1f;
    private float _minColorValue = 0f;

    public void ChangeColor(MeshRenderer renderer)
    {
        renderer.material.color = GenerateColor();
    }

    private Color GenerateColor() =>
        new Color
        (
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue)
            );
}
