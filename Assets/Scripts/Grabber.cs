using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
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
        if (Input.GetKey(_takeKey))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            FindGround(ray);

            FindDraggableObject(ray);

            DragObjectToPosition();
        }
        else
        {
            _currentObject = null;
        }

    }

    private void FindGround(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit groundHitInfo, RayDistance, _ground.value))
        {
            _groundPoint = groundHitInfo.point;
        }
    }

    private void FindDraggableObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit colliderHitInfo, RayDistance))
        {
            if (colliderHitInfo.collider.gameObject.TryGetComponent<IDraggable>(out IDraggable draggable))
            {
                _currentObject = draggable.ObjectTransform;
            }
        }
    }

    private void DragObjectToPosition()
    {
        if (_currentObject != null)
        {
            _currentObject.position = new Vector3(_groundPoint.x, _yUpPosition, _groundPoint.z);
        }
    }
}
