using UnityEngine;

namespace Explosion
{
    internal sealed class Explosion : MonoBehaviour
    {
        public void PlayParticles()
        {
            var children = transform.GetComponentsInChildren<ParticleSystem>();
            for (var i = 0; i < children.Length; ++i)
            {
                children[i].Play();
            }
            var current = GetComponent<ParticleSystem>();
            if (current != null) current.Play();
        }
    }
}