using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerPosition : MonoBehaviour {

    private Transform player;

    // Use this for initialization
    void Start () {
        player = Player.Instance.transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
	}
}
