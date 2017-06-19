using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieController : EnemyBaseController
{
    private float currentDeadTime;
    public float destroyTime;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        currentDeadTime = 0;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if (dead)
        {
            currentDeadTime += Time.deltaTime;
            if (currentDeadTime > destroyTime) {
                Destroy(gameObject);
            }
        }
	}

    protected override void Dead()
    {
        base.Dead();
        animator.SetTrigger("Dead");
    }
}
