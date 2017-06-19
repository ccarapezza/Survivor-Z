using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Small,
    Normal,
    Large
}

public enum GunType
{
    Semiauto,
    Auto
}

public abstract class Gun : MonoBehaviour {
    public ParticleSystem shootTrace;
    public BulletType bulletType;
    public GunType gunType;
    public int bullets;

    public abstract void PullTrigger();

    public abstract void ReleaseTrigger();

    public virtual void Shoot()
    {
        if (bullets > 0)
        {
           print("Shot!");
            Ray ray = new Ray(GunsManager.Instance.gunBarrel.position, GunsManager.Instance.gunBarrel.forward);
            Debug.DrawRay(ray.origin, ray.direction*10, Color.green, 0.2f);

            GunsManager.Instance.gunBarrel.GetComponent<ParticleSystem>().Play();

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemys"))
                {
                    EnemyBaseController enemy = hit.collider.GetComponent<EnemyBaseController>();
                    enemy.Damage(CalculateDamage(bulletType), hit.point, ray.direction);
                }
            }
            GunHudController.Instance.ChangeBulletCount(--bullets);
        }
    }

    protected int CalculateDamage(BulletType bulletType)
    {
        if (bulletType == BulletType.Small)
            return 2;
        if (bulletType == BulletType.Normal)
            return 5;
        if (bulletType == BulletType.Large)
            return 10;
        return 0;
    }
}
