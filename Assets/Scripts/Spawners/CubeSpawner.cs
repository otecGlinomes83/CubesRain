using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class CubeSpawner : GenericSpawner<ColorCube>
    {
        [SerializeField] private float _spawnRate;

        public event Action<Vector3> CubeReleased;

        private Collider _spawnZone;

        protected override void Awake()
        {
            base.Awake();
            _spawnZone = GetComponent<Collider>();
        }

        private void Start()
        {
            StartCoroutine(GetCubeFromPool());
        }

        private IEnumerator GetCubeFromPool()
        {
            while (enabled)
            {
                GetObject();
                yield return new WaitForSecondsRealtime(_spawnRate);
            }
        }

        protected override void Deactivate(ColorCube cube)
        {
            base.Deactivate(cube);
            cube.ReadyForRelease -= _objectPool.Release;
            CubeReleased?.Invoke(cube.transform.position);
        }

        protected override void Spawn(ColorCube cube)
        {
            base.Spawn(cube);

            cube.transform.position = new Vector3
    (
    UnityEngine.Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x),
    UnityEngine.Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y),
    UnityEngine.Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z)
    );

            cube.SetDefault();
            cube.ReadyForRelease += _objectPool.Release;
        }
    }
}