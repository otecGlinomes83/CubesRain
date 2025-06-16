using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private float _spawnRate;

    private List<Cube> _allCubes = new List<Cube>();

    private ObjectPool<Cube> _cubePool;
    private Collider _spawnZone;

    private void Awake()
    {
        _spawnZone = GetComponent<Collider>();

        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => SpawnCubeRandomPosition(cube),
            actionOnRelease: (cube) => DeactivateCube(cube));
    }

    private void Start()
    {
        StartCoroutine(nameof(GetCubeFromPool));
    }

    private void OnDisable()
    {
        foreach (Cube cube in _allCubes)
        {
            cube.ReadyForRelease -= _cubePool.Release;
        }
    }

    private IEnumerator GetCubeFromPool()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSecondsRealtime(_spawnRate);

            _cubePool.Get();
        }
    }

    private void SpawnCubeRandomPosition(Cube cube)
    {
        cube.transform.position = new Vector3
            (
            Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x),
            Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y),
            Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z)
            );

        cube.SetDefault();

        cube.gameObject.SetActive(true);

        cube.ReadyForRelease += _cubePool.Release;
    }

    private void DeactivateCube(Cube cube)
    {
        cube.ReadyForRelease -= _cubePool.Release;
        cube.gameObject.SetActive(false);
    }
}