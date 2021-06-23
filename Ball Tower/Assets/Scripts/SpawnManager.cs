using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private List<GameObject> platforms;
    [SerializeField]
    private List<GameObject> powerups;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<GameObject> allSpawns;
    [SerializeField]
    private GameObject player;
    private Transform playerTransform;
    private float spawnMax = 6.4f;
    private float spawnMin = -6.4f;
    private float spawnDistance = 1.5f;
    private float spawnZPosition = 10.0f;
    [SerializeField]
    private float powerupChanceValue;
    [SerializeField]
    private float enemyChanceValue;

    [SerializeField]
    private float hight = 0;
    public float Hight { get { return hight; } }
    [SerializeField]
    private float addHight = 1.5f;

    int platformsCapacity;

    private void Start()
    {
        GameManager.Instance.onStartGame += SetInactiveAllSpawns;
        GameManager.Instance.onStartGame += CreateFirstPlatforms;
        GameManager.Instance.onStartGame += RestartHight;
        playerTransform = player.GetComponent<Transform>();
        platformsCapacity = platforms.Capacity;      
    }

    private void Update()
    {
        if (playerTransform.position.y > hight) 
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        int randomPlatform = Random.Range(0, platformsCapacity);
        float randomSpawnPosition = Random.Range(spawnMin, spawnMax);
        SpawnPlatform(randomPlatform, randomSpawnPosition, player.transform.position.y, 5);
        hight += addHight;
    }

    public void SetInactiveAllSpawns()
    {
        foreach(var platform in allSpawns)
        {
            platform.SetActive(false);
        }
    }

    public void CreateFirstPlatforms()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomPlatform = Random.Range(0, platformsCapacity);
            float randomSpawnPosition = Random.Range(spawnMin, spawnMax);
            SpawnPlatform(randomPlatform, randomSpawnPosition, 0, i + 1);
        }
    }

    private void SpawnPlatform(int randomPlatform, float randomSpawnPosition, float offset, float distance)
    {
        GameObject newPlatform;
        bool isPooling = false;
        foreach (GameObject spawn in allSpawns)
        {
            if (spawn.name == $"{platforms[randomPlatform].name}(Clone)" && !spawn.activeInHierarchy && isPooling == false) 
            {
                newPlatform = spawn;
                newPlatform.transform.position = new Vector3(randomSpawnPosition, offset + spawnDistance * distance, spawnZPosition);
                newPlatform.SetActive(true);
                isPooling = true;
            }
        }
        if (isPooling == false)
        {
            newPlatform = Instantiate(platforms[randomPlatform], new Vector3(randomSpawnPosition, offset + spawnDistance * distance, spawnZPosition), Quaternion.identity);
            allSpawns.Add(newPlatform);
        }

        float powerupChance = Random.Range(0, 100);
        float EnemyChance = Random.Range(0, 100);
        if (powerupChance >= 100 - powerupChanceValue)
        {
            SpawnPowerUp(randomSpawnPosition,offset,distance);
        }
        else if (EnemyChance >= 100 - enemyChanceValue)
        {
            SpawnEnemy(randomSpawnPosition,offset,distance);
        }
    }

    private void SpawnPowerUp(float randomSpawnPosition, float offset, float distance)
    {
        GameObject newPowerup;
        foreach(GameObject spawn in allSpawns)
        {
            if (spawn.name == $"{powerups[0]}(Clone)" && !spawn.activeInHierarchy)
            {
                newPowerup = spawn;
                newPowerup.transform.position = new Vector3(randomSpawnPosition, offset + spawnDistance * distance + 0.5f, spawnZPosition);
                newPowerup.SetActive(true);
                return;
            }
        }
        newPowerup = Instantiate(powerups[0], new Vector3(randomSpawnPosition, offset + spawnDistance * distance + 0.5f, spawnZPosition), Quaternion.identity);
        allSpawns.Add(newPowerup);
    }

    private void SpawnEnemy(float randomSpawnPosition, float offset, float distance)
    {
        GameObject newEnemy;
        foreach (GameObject spawn in allSpawns)
        {
            if (spawn.name == $"{enemies[0]}(Clone)" && !spawn.activeInHierarchy)
            {
                newEnemy = spawn;
                newEnemy.transform.position = new Vector3(randomSpawnPosition, offset + spawnDistance * distance + 0.5f, spawnZPosition);
                newEnemy.SetActive(true);
                return;
            }
        }
        newEnemy = Instantiate(enemies[0], new Vector3(randomSpawnPosition, offset + spawnDistance * distance + 0.7f, spawnZPosition), Quaternion.identity);
        allSpawns.Add(newEnemy);
    }

    private void RestartHight()
    {
        hight = 0;
    }
}
