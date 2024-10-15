using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShooter
{
    private const float RayDistance = 100f;

    private Camera _camera;

    private KeyCode _explosionKey;
    private ParticleSystem _particleSystem;

    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;

    public ExplosionShooter(Camera camera, KeyCode explosionKey, ParticleSystem particleSystem, float explosionForce, float explosionRadius)
    {
        _camera = camera;
        _explosionKey = explosionKey;
        _particleSystem = particleSystem;
        _explosionForce = explosionForce;
        _explosionRadius = explosionRadius;
    }

    public void Explode()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(_explosionKey))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, RayDistance))
            {
                _particleSystem.transform.position = hitInfo.point;
                _particleSystem.Play();

                Collider[] colliders = Physics.OverlapSphere(hitInfo.point, _explosionRadius);

                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        rigidbody.AddExplosionForce(_explosionForce, hitInfo.point, _explosionRadius);
                    }
                }
            }
        }

    }
}
