using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Explosion
{
    internal sealed class AtomicBomb : MonoBehaviour
    {
        [SerializeField] private float _shakeStreight;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;

        [Space]

        [SerializeField] private Explosion _explosionPrefab;
        [SerializeField] private StressReceiver _camera;

        private void OnCollisionEnter(Collision collision)
        {
            AddExplosionForce();

            InstantiateParticles();
            ShakeCamera();

            Destroy(gameObject);
        }

        private void AddExplosionForce()
        {
            List<Collider> colliders = Physics.OverlapSphere(transform.position, _explosionRadius).ToList();
            List<Rigidbody> rigidbodies = SortHittedObject(colliders);

            print(rigidbodies.Count);

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 5f, ForceMode.Impulse);
            }
        }

        private List<Rigidbody> SortHittedObject(List<Collider> colliders)
        {
            List<Rigidbody> rigidbodies = new();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbodies.Add(rigidbody);
                }
            }

            return rigidbodies;
        }

        private void InstantiateParticles()
        {
            Explosion instance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            instance.PlayParticles();
        }

        private void ShakeCamera()
        {
            _camera.InduceStress(_shakeStreight);
        }
    }
}