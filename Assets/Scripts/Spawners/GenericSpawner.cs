using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Spawners
{
    public class GenericSpawner<T> : MonoBehaviour where T : FallingObject
    {
        [SerializeField] protected T _prefab;

        protected ObjectPool<T> _objectPool;

        protected float _spawnCount = 0f;
        protected float _instantiateCount = 0f;

        public float SpawnCount => _spawnCount;
        public float InstantiateCount => _instantiateCount;
        public float ActiveCount => _objectPool.CountActive;

        protected virtual void Awake()
        {
            _objectPool = new ObjectPool<T>
                (
                createFunc: () => Create(),
                actionOnGet: (figure) => Spawn(figure),
                actionOnRelease: (figure) => Deactivate(figure)
                );
        }

        protected virtual T Create()
        {
            _instantiateCount++;
            return Instantiate(_prefab);
        }

        protected virtual void GetObject()
        {
            _objectPool.Get();
        }

        protected virtual void Deactivate(T figure)
        {
            figure.gameObject.SetActive(false);
        }

        protected virtual void Spawn(T figure)
        {
            figure.gameObject.SetActive(true);
            _spawnCount++;
        }
    }
}