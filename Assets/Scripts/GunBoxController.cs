using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBoxController : MonoBehaviour {

    public Gun gun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GunsManager.Instance.AddGun(gun);
            Destroy(gameObject);
        }
    }
}
