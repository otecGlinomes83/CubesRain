using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Exploder : MonoBehaviour
    {
        [SerializeField] private float _explodeForce;
        [SerializeField] private float _explodeRadius;

        public void Explode()
        {
            foreach (Rigidbody rigidbody in GetExplodingObjects())
            {
                rigidbody.AddExplosionForce(_explodeForce, transform.position, _explodeRadius, 5f, ForceMode.Impulse);
            }
        }

        private List<Rigidbody> GetExplodingObjects()
        {
            return Physics.OverlapSphere(transform.position, _explodeRadius)
                .Where(obj => obj.GetComponent<Rigidbody>() != null)
                .Select(rigidbody => rigidbody.GetComponent<Rigidbody>())
                .ToList();
        }
    }
}