using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoGun : Gun
{
    private bool fired;

    public override void PullTrigger()
    {
        if (!fired && bullets>0) {
            Player.Instance.animatorController.SingleFire();
            fired = true;
        }
    }

    public override void ReleaseTrigger()
    {
        fired = false;
    }
}
