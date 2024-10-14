using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBox : Obstacle, IInteractable, IExplosion
{
    public void Interact(Vector3 moveDirection)
    {
        transform.position = moveDirection;
    }
}
