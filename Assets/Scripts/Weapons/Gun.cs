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
    public Transform gunBarrel;
    public GameObject bulletPrefab;

    public abstract void PullTrigger();

    public abstract void ReleaseTrigger();

    public virtual void Shoot()
    {
        if (bullets > 0)
        {
            Ray ray = new Ray(gunBarrel.position, gunBarrel.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.GetComponent<Interpolation>().destination = hit.point;
                bullet.transform.position = gunBarrel.position;
                bullet.transform.forward = ray.direction;

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
