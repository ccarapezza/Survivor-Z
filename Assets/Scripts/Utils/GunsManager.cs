using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsManager : MonoBehaviour {
    private static GunsManager instance;
    public static GunsManager Instance { get { return instance; } }

    public List<Gun> prefabWeapons;
    private List<Gun> guns;
    private int currentGun;
    public Transform gunBarrel;

    // Use this for initialization
    void Awake () {
        currentGun = 0;
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        //gunBarrel = transform.parent;
        guns = new List<Gun>();
        GameObject rightHand = GameObject.FindGameObjectWithTag("RightHand");
        foreach (var prefabWeapon in prefabWeapons)
        {
            Gun weapon = Instantiate(prefabWeapon, rightHand.transform);
            weapon.transform.localRotation = Quaternion.identity;

            //weapon.transform.localEulerAngles = Vector3.zero;
            weapon.gameObject.SetActive(false);
            guns.Add(weapon);
        }

        guns[0].gameObject.SetActive(true);

    }

    void Start()
    {
        //Player.Instance.gun = CurrentGun();
        gunBarrel = guns[0].gunBarrel;
        UpdateGunHud(CurrentGun());
    }

    public Gun CurrentGun()
    {
        return guns[currentGun];
    }

    public Gun NextGun()
    {
        guns[currentGun].gameObject.SetActive(false);
        if (++currentGun > guns.Count - 1)
            currentGun = 0;
        guns[currentGun].gameObject.SetActive(true);
        UpdateGunHud(guns[currentGun]);
        return guns[currentGun];
    }

    public Gun PrevGun()
    {
        guns[currentGun].gameObject.SetActive(false);
        if (--currentGun < 0)
            currentGun = guns.Count - 1;
        guns[currentGun].gameObject.SetActive(true);
        UpdateGunHud(guns[currentGun]);
        return guns[currentGun];
    }

    public void AddGun(Gun gun)
    {
        GameObject rightHand = GameObject.FindGameObjectWithTag("RightHand");
        Gun weapon = Instantiate(gun, rightHand.transform);
        weapon.transform.localRotation = Quaternion.identity;

        //weapon.transform.localEulerAngles = Vector3.zero;
        weapon.gameObject.SetActive(false);
        guns.Add(weapon);
    }

    private void UpdateGunHud(Gun gun)
    {
        GunHudController.Instance.ChangeBulletCount(gun.bullets);
        GunHudController.Instance.ChangeBulletIcon(gun.bulletType);
        GunHudController.Instance.ChangeGunIcon(gun.gunType);
    }
}