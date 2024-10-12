using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private LayerMask _ground;

    private Vector3 _groundPoint;

    private Transform _currentObject;
    

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * 100f, Color.green);

        if (Physics.Raycast(ray, out RaycastHit groundHitInfo, 100f, _ground.value))
        {
            _groundPoint = groundHitInfo.point;
        }

        if (Physics.Raycast(ray, out RaycastHit colliderHitInfo, 100f))
        {
            if (colliderHitInfo.collider.gameObject.TryGetComponent<Box>(out Box box))
            {
                Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * 100f, Color.red);

                if (Input.GetMouseButton(0))
                {
                    _currentObject = box.transform;
                }
                else
                {
                    _currentObject = null;
                }
            }
        }

        if (_currentObject != null)
        {
            _currentObject.position = new Vector3(_groundPoint.x, 2f, _groundPoint.z);
        }
    }
}
