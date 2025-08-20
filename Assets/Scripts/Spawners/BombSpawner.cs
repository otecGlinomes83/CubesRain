using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class BombSpawner : GenericSpawner<Bomb>
    {
        [SerializeField] private CubeSpawner _cubeSpawner;

        private void OnEnable()
        {
            _cubeSpawner.CubeReleased += OnCubeReleased;
        }

        private void OnDisable()
        {
            _cubeSpawner.CubeReleased -= OnCubeReleased;
        }

        private void OnCubeReleased(Vector3 cubePosition)
        {
            Bomb bomb = _objectPool.Get();
            bomb.transform.position = cubePosition;
        }

        protected override void Spawn(Bomb figure)
        {
            figure.SetDefault();
            base.Spawn(figure);
            figure.ReadyForRelease += _objectPool.Release;
        }

        protected override void Deactivate(Bomb figure)
        {
            base.Deactivate(figure);
            figure.ReadyForRelease -= _objectPool.Release;
        }
    }
}