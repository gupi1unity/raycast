using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float RayDistance = 100f;

    private Camera _camera;

    [SerializeField] private KeyCode _explosionKey = KeyCode.Mouse1;
    [SerializeField] private int _damage = 10;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, RayDistance))
        {
            Collider[] colliders = Physics.OverlapSphere(hitInfo.point, 5f);

            if (Input.GetKeyDown(_explosionKey))
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable gameObject))
                    {
                        gameObject.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}
