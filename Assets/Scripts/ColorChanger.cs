using UnityEngine;

public class ColorChanger
{
    private float _maxColorValue = 1f;
    private float _minColorValue = 0f;

    public void SetRandomColor(MeshRenderer renderer)
    {
        renderer.material.color = GenerateColor();
    }

    public void SetColor(MeshRenderer renderer, Color color)
    {
        renderer.material.color = color;
    }

    public void SetAlpha(MeshRenderer renderer, float value)
    {
        Color color = renderer.material.color;
        color.a = value;
        renderer.material.color = color;
    }

    private Color GenerateColor() =>
        new Color
        (
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue),
            Random.Range(_minColorValue, _maxColorValue)
            );
}