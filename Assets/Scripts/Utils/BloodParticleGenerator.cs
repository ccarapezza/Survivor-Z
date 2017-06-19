using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleGenerator : MonoBehaviour {
    private static BloodParticleGenerator instance;
    public static BloodParticleGenerator Instance { get { return instance; } }

    public ParticleSystem bloodSpashPrefab;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void GenerateBloodSplash(Transform parent, Vector3 position, Vector3 direction)
    {
        ParticleSystem bloodSplash = Instantiate(bloodSpashPrefab);
        bloodSplash.transform.parent = parent;
        bloodSplash.transform.position = position;
        bloodSplash.transform.forward = -direction;
        /*bloodSplash.transform.parent = enemy.transform;
        bloodSplash.transform.position = hit.point;
        bloodSplash.transform.forward = -ray.direction;*/
    }
}
