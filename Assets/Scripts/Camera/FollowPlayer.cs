using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private Transform player;
    private Vector3 playerPos;
    float deadZone = 0.15f;
    float speed = 1.5f;

    void Start()
    {
        player = Player.Instance.transform;
    }

    void Update()
    {
        if (Player.Instance != null)
        {
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
            playerPos = player.position;
            playerPos += Vector3.forward * 0f;
            playerPos.y = playerPos.y + 8;

            if (Vector3.Distance(playerPos, player.position) > deadZone)
                transform.position = Vector3.Lerp(transform.position, playerPos, speed * Time.deltaTime);
        }
    }
}
