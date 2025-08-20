using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    [RequireComponent(typeof(Exploder))]
    public class Bomb : FallingObject
    {
        public event Action<Bomb> ReadyForRelease;

        private Exploder _exploder;

        private Coroutine _delayedReleaseCoroutine;

        private float _targetAlpha = 0f;
        private float _currentAlpha = 1f;

        protected override void Awake()
        {
            base.Awake();
            _exploder = GetComponent<Exploder>();
            _colorChanger.SetAlpha(_meshRenderer, _currentAlpha);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (_delayedReleaseCoroutine != null)
                return;

            _delayedReleaseCoroutine = StartCoroutine(DelayedRelease(GenerateDelay()));
        }

        public override void SetDefault()
        {
            base.SetDefault();

            _currentAlpha = 1f;
            _colorChanger.SetAlpha(_meshRenderer, _currentAlpha);
        }

        protected override IEnumerator DelayedRelease(float time)
        {
            _isReleasing = true;

            WaitForSecondsRealtime step = new WaitForSecondsRealtime(1f);

            float alphaStep = 1f / time;

            for (int i = 0; i < time; i++)
            {
                yield return step;

                _currentAlpha = Mathf.MoveTowards(_currentAlpha, _targetAlpha, alphaStep);
                _colorChanger.SetAlpha(_meshRenderer, _currentAlpha);
            }

            _exploder.Explode();

            ReadyForRelease?.Invoke(this);
            _isReleasing = false;

            _delayedReleaseCoroutine = null;
        }
    }
}