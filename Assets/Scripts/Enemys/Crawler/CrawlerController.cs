using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerController : EnemyBaseController
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }
}
