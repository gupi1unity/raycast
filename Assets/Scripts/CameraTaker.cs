using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTaker : MonoBehaviour
{
    private const float RayDistance = 100f;

    private Camera _camera;

    [SerializeField] private KeyCode _takeKey = KeyCode.Mouse0;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _yUpPosition = 2f;
    private Vector3 _groundPoint;
    private Transform _currentObject;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        CheckButtonUp();

        Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * RayDistance, Color.green);

        CreateRayToGround(ray);

        CreateRayToObject(ray);

        CheckObjectNull();
    }

    private void CheckButtonUp()
    {
        if (!Input.GetKey(_takeKey))
            _currentObject = null;
    }

    private void CreateRayToGround(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit groundHitInfo, RayDistance, _ground.value))
        {
            _groundPoint = groundHitInfo.point;
        }
    }

    private void CreateRayToObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit colliderHitInfo, RayDistance))
        {
            if (colliderHitInfo.collider.gameObject.TryGetComponent<Obstacle>(out Obstacle box) && colliderHitInfo.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * RayDistance, Color.red);

                if (Input.GetKey(_takeKey))
                {
                    _currentObject = box.transform;
                }
            }
        }
    }

    private void CheckObjectNull()
    {
        if (_currentObject != null)
        {
            _currentObject.position = new Vector3(_groundPoint.x, _yUpPosition, _groundPoint.z);
        }
    }
}
