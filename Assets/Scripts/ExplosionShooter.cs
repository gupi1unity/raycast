using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShooter : MonoBehaviour
{
    private const float RayDistance = 100f;

    private Camera _camera;

    [SerializeField] private KeyCode _explosionKey = KeyCode.Mouse1;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, RayDistance))
        {
            if (Input.GetKeyDown(_explosionKey))
            {
                Instantiate(_particleSystem, hitInfo.point, Quaternion.identity);

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
