using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyHurt : MonoBehaviour
{
    public void EnemyDestroy()
    {
        EnemyDead();
        Destroy(gameObject);
    }

    public abstract void EnemyDead();
}
