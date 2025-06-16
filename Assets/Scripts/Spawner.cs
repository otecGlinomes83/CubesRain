using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<HitDetector> _cubeDetectors;

    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private float _minCoordinateValue = -10f;
    private float _maxCoordinateValue = 10f;

    private ColorChanger _colorChanger = new ColorChanger();

    private ObjectPool<Cube> _cubePool;
    private Color _originalColor;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => GetCube(cube),
            actionOnRelease: (cube) => Release(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        MeshRenderer prefabMeshRenderer = _cubePrefab.GetComponent<MeshRenderer>();

        _originalColor = prefabMeshRenderer.sharedMaterial.color;
    }

    private void OnEnable()
    {
        foreach (var detector in _cubeDetectors)
        {
            detector.CubeDetected += OnCubeDetected;
        }
    }

    private void OnDisable()
    {
        foreach (var detector in _cubeDetectors)
        {
            detector.CubeDetected -= OnCubeDetected;
        }
    }

    private void GetCube(Cube cube)
    {
        cube.transform.position = new Vector3(Random.Range(_minCoordinateValue, _maxCoordinateValue), 20f, Random.Range(_minCoordinateValue, _maxCoordinateValue));

        cube.gameObject.SetActive(true);

        cube.MeshRenderer.material.color = _originalColor;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0f, _spawnRate);
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private void Release(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void OnCubeDetected(Cube cube)
    {
        if (cube.IsReleasing)
            return;

        _colorChanger.ChangeColor(cube.MeshRenderer);

        StartCoroutine(DelayedRelease(cube, cube.GenerateDelay()));
    }

    private IEnumerator DelayedRelease(Cube cube, float delay)
    {
        cube.SetReleasing(true);
        yield return new WaitForSeconds(delay);
        _cubePool.Release(cube);
        cube.SetReleasing(false);
    }
}