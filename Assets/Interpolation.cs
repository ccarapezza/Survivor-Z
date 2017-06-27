using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour {
    public float speed;
    public Vector3 destination;
    private float timeStarted;
    private float timeShoot;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        timeStarted = Time.time;
        timeShoot = Vector3.Distance(initialPosition, new Vector3(destination.x, transform.position.y, destination.z))/speed;
    }

    // Update is called once per frame
    void Update () {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / timeShoot;
        transform.position = Vector3.Lerp(initialPosition, destination, percentageComplete);
        if (percentageComplete > 1)
            Destroy(gameObject);
	}
}
