using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : Gun
{
    public float shootMultiplier;

    public float maxOverHeatingTime;
    public float overHeatingTime;

    private bool overheated;
    private bool isShooting;

    public override void PullTrigger()
    {
        isShooting = true;
        Player.Instance.animatorController.StartFire(shootMultiplier);
    }

    public override void ReleaseTrigger()
    {
        isShooting = false;
        Player.Instance.animatorController.StopFire();
    }

    public override void Shoot()
    {
        if (!overheated) {
            base.Shoot();
        }
    }

    void Update()
    {
        if (isShooting && !overheated)
        {
            overHeatingTime += Time.deltaTime;
            overHeatingTime = Mathf.Clamp(overHeatingTime, 0, maxOverHeatingTime);
            GunHudController.Instance.ChangeGunTempBar(overHeatingTime / maxOverHeatingTime);
            if (overHeatingTime >= maxOverHeatingTime)
            {
                overheated = true;
            }
        }
        else
        {
            overHeatingTime -= Time.deltaTime;
            overHeatingTime = Mathf.Clamp(overHeatingTime, 0, maxOverHeatingTime);
            GunHudController.Instance.ChangeGunTempBar(overHeatingTime / maxOverHeatingTime);
        }
        if (overheated)
        {
            if (overHeatingTime == 0)
            {
                overheated = false;
            }
        }
    }
}
