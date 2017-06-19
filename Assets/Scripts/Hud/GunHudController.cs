using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHudController : MonoBehaviour {
    private static GunHudController instance;
    public static GunHudController Instance { get { return instance; } }

    public List<Sprite> gunIcons;
    public List<Sprite> bulletIcons;

    public Image gunTempBar;
    public Image gunIcon;
    public Image bulletIcon;

    public Text bulletCount;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void ChangeGunIcon(GunType gunType)
    {
        gunIcon.sprite = gunIcons[(int)gunType];
    }

    public void ChangeBulletIcon(BulletType bulletType)
    {
        bulletIcon.sprite = bulletIcons[(int)bulletType];
    }

    public void ChangeBulletCount(int bullets)
    {
        bulletCount.text = bullets.ToString();
    }

    public void ChangeGunTempBar(float gunTemp)
    {
        //print("TOTAL GUNHUD! " + gunTemp);
        gunTempBar.fillAmount = gunTemp;
    }
}
