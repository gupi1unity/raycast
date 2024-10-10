using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private Camera _camera;
    

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * 100f, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<IMovable>(out IMovable gameObject))
            {
                Debug.DrawRay(_camera.ScreenToWorldPoint(Input.mousePosition), _camera.transform.forward * 100f, Color.red);

                if (Input.GetMouseButton(0))
                {
                    Vector3 moveDirection = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
                    gameObject.Move(moveDirection);
                }
            }
        }
    }
}
