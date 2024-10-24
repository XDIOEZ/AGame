using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPTP_Child : EnemyHurt
{
    public override void EnemyDead()
    {
        transform.GetComponentInParent<GhostPTP>().EnemyDead();
    }
}
