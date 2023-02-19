using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public GameObject spawnable;
    public GameObject winSpawnable;

    public float spawnTime;
    public float winSpawnTime;

    public float spawnTimeIncrease;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        InvokeRepeating("WinSpawn", winSpawnTime, winSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime = spawnTime * spawnTimeIncrease;
        winSpawnTime = winSpawnTime * spawnTimeIncrease;
    }
    void Spawn()
    {
        var position = new Vector3(Random.Range(7, -7), 7, Random.Range(7, -7));
        Instantiate(spawnable, position, Quaternion.identity);
       // Destroy(spawnable, 5.0f); how do i destroy the spawned instances after 5 seconds, not the prefab that's being cloned?
    }

    void WinSpawn()
    {
        var position = new Vector3(Random.Range(7, -7), 7, Random.Range(7, -7));
        Instantiate(winSpawnable, position, Quaternion.identity);
      //  Destroy(winSpawnable, 5.0f);
    }
}
