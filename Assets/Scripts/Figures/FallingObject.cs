using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    public abstract class FallingObject : MonoBehaviour
    {
        protected Rigidbody _rigidbody;
        protected MeshRenderer _meshRenderer;
        protected ColorChanger _colorChanger;

        protected bool _isReleasing = false;

        protected int _minLifeTime = 2;
        protected int _maxLifeTime = 5;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _colorChanger = new ColorChanger();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<Platform>(out _) == false)
                return;

            if (_isReleasing)
                return;

            StartCoroutine(DelayedRelease(GenerateDelay()));
        }

        public virtual void SetDefault()
        {
            transform.rotation = Quaternion.identity;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        protected abstract IEnumerator DelayedRelease(float delay);

        protected float GenerateDelay() =>
    UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
    }
}