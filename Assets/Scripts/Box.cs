using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IMovable
{
    public void Move(Vector3 moveDirection)
    {
        transform.position = moveDirection;
    }
}
