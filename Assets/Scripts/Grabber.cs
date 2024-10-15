using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber
{
    private const float RayDistance = 100f;

    private Camera _camera;

    private KeyCode _grabKey;
    private LayerMask _ground;
    private float _yUpPosition;
    private Vector3 _groundPoint;
    private Transform _currentObject;

    public Grabber(Camera camera, KeyCode grabKey, LayerMask ground, float yUpPosition)
    {
        _camera = camera;
        _grabKey = grabKey;
        _ground = ground;
        _yUpPosition = yUpPosition;
    }

    public void Grab()
    {
        if (Input.GetKey(_grabKey))
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
