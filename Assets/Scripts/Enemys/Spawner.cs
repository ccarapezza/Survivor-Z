using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {
    private static Spawner instance;
    public static Spawner Instance { get { return instance; } }

    public GameObject zombie1Prefab;
    public GameObject zombie2Prefab;
    public float inset;

    public List<WaveData> waves;

    private int wavesLeft;
    private int currentWave;
    private int zombiesAlive;
    private bool waveSpawnFinish;

    Vector3 limitsMin;
    Vector3 limitsMax;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start() {
        currentWave = 1;
        StartCoroutine(ZombieWaveGeneratorCoroutine(waves[0]));
        waveSpawnFinish = false;
    }

    // Update is called once per frame
    void Update() {
        limitsMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.y));
        limitsMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.transform.position.y));
    }

    public void ZombieDead() {
        zombiesAlive--;
        if (waveSpawnFinish && zombiesAlive < 1) {
            waves.RemoveAt(0);
            if (waves.Count > 0)
            {
                StartCoroutine(ZombieWaveGeneratorCoroutine(waves[0]));
            }
            else
            {
                StartCoroutine(FinishLevel());
            }
        }
    }

    IEnumerator FinishLevel()
    {
        MessageCanvasManager.Instance.ShowCanvasMessage("SECTOR CLEARED!", 4);
        yield return new WaitForSeconds(5f);
        LevelSceneManager.Instance.NextLevel();
    }

    IEnumerator ZombieWaveGeneratorCoroutine(WaveData wave/*int zombie1Count, int zombie2Count, int minSimultaneousSpawns, int maxSimultaneousSpawns*/) {
        MessageCanvasManager.Instance.ShowCanvasMessage("WAVE "+currentWave++, 4);
        yield return new WaitForSeconds(4f);
        waveSpawnFinish = false;
        int totalZombies = wave.zombie1Count + wave.zombie2Count;
        List<int> spawnsRounds = new List<int>();
        spawnsRounds.Add(Mathf.CeilToInt(totalZombies * 0.2f));
        spawnsRounds.Add(Mathf.CeilToInt(totalZombies * 0.3f));
        spawnsRounds.Add(totalZombies - spawnsRounds[0] - spawnsRounds[1]);

        while (wave.zombie1Count > 0 || wave.zombie2Count > 0) {
            int zombieIndexToSpawn = Random.Range(0, 2);
            if (wave.zombie1Count < 1 && wave.zombie2Count>1)
                zombieIndexToSpawn = 1;
            if (wave.zombie2Count < 1 && wave.zombie1Count > 1)
                zombieIndexToSpawn = 0;

            int min = (wave.zombie1Count + wave.zombie2Count < wave.minSimultaneousSpawns) ? wave.zombie1Count + wave.zombie2Count : wave.minSimultaneousSpawns;
            int max = (wave.zombie1Count + wave.zombie2Count < wave.maxSimultaneousSpawns) ? wave.zombie1Count + wave.zombie2Count : wave.minSimultaneousSpawns;




            foreach (var spawnsRound in spawnsRounds)
            {
                int zombieSpawnCount = spawnsRound;
                while (zombieSpawnCount > 0)
                {
                    GameObject zombiePrefab;
                    if (zombieIndexToSpawn == 0)    
                    {
                        wave.zombie1Count--;
                        zombiePrefab = zombie1Prefab;
                    }
                    else
                    {
                        wave.zombie2Count--;
                        zombiePrefab = zombie2Prefab;
                    }
                    SpawnZombieOutsideOfCamera(zombiePrefab);
                    zombiesAlive++;
                    zombieSpawnCount--;
                    yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
                }
            }

            while (zombiesAlive < 0)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        waveSpawnFinish = true;
    }

    void SpawnZombieOutsideOfCamera(GameObject zombiePrefab)
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

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 10.0f, NavMesh.AllAreas))
        {
            GameObject zombie = Instantiate(zombiePrefab);
            zombie.transform.position = hit.position;
        }
    }
}


[System.Serializable]
public class WaveData {
    public int zombie1Count;
    public int zombie2Count;
    public int minSimultaneousSpawns;
    public int maxSimultaneousSpawns;
}