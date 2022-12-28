using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float _explosionRadius = 5f;
    [SerializeField]
    private float _explostionForce = 5f;
    [SerializeField]
    private ParticleSystem particles;

    protected void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out IHittable hittable))
                hittable.Hit(_explostionForce);

            if (col.TryGetComponent(out Rigidbody body))
                body.AddExplosionForce(_explostionForce, transform.position, _explosionRadius);
        }
        Instantiate(particles, transform.position, Quaternion.identity);
        particles.Play();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
