using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<GameObject> _cubePool;

    private ColorChanger _colorChanger = new ColorChanger();

    private void Awake()
    {
        _cubePool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }


    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 15f, UnityEngine.Random.Range(-5f, 5f));
        cube.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0f, _repeatRate);
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cube cube;

        if (other.TryGetComponent<Cube>(out cube) == false)
            return;

        _colorChanger.ChangeColor(cube.MeshRenderer);

        StartCoroutine(DelayedRelease(other.gameObject, cube.GenerateDelay()));
    }

    private IEnumerator DelayedRelease(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        _cubePool.Release(gameObject);
    }
}
