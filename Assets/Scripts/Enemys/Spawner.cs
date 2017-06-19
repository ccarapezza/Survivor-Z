using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject zombiePrefab;
    public float inset;

    Vector3 limitsMin;
    Vector3 limitsMax;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        limitsMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.y));
        limitsMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.transform.position.y));
        print(limitsMin);
        print(limitsMax);
        if (Input.GetKeyUp(KeyCode.T))
        {
            print("ZOMBIE!!");
            Vector3 randomPos = new Vector3(limitsMax.x,5, limitsMax.z);
            GameObject zombie = Instantiate(zombiePrefab);
            zombie.transform.position = randomPos;
            //RandomPosOutsideCamera();
        }
    }

    void RandomPosOutsideCamera()
    {
        Vector3 randomPos = Vector3.zero;
        randomPos.y = 5;

        if (Random.Range(0, 2)==1)
        {
            randomPos.x = Random.Range(limitsMin.x - inset, limitsMax.x + inset);
            if (Random.Range(0, 2) == 1)
                randomPos.z = limitsMax.z + inset;
            else
                randomPos.z = limitsMin.z - inset;
        }
        else
        {
            randomPos.z = Random.Range(limitsMin.z - inset, limitsMax.z + inset);
            if (Random.Range(0, 2) == 1)
                randomPos.x = limitsMin.x - inset;
            else
                randomPos.x = limitsMax.x + inset;
        }
        
        GameObject zombie = Instantiate(zombiePrefab);
        zombie.transform.position = randomPos;
    }
}