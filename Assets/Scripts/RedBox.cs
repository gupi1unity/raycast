using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBox : Obstacle, IDamagable
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
