using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public Transform[] spawn_points;
    public GameObject enemy_prefab;

    private const int max_enemies = 10;
    private float time_elapsed = 0;
    private float time_to_spawn;

    private void Awake()
    {
        instance = this;
        time_to_spawn = float.MaxValue;
    }

    public void StartGame()
    {
        time_to_spawn = Random.Range(20, 50);
    }

    public void StopGame()
    {
        for (int i = 0; i < spawn_points.Length; i++)
        {
            for(int y = 0; y < spawn_points[i].childCount; y++)
            {
                Destroy(spawn_points[i].GetChild(y).gameObject);
            }
        }
        time_to_spawn = float.MaxValue;
    }

    private void Update()
    {
        time_elapsed += Time.deltaTime;
        if (time_elapsed >= time_to_spawn)
        {
            SpawnEnemies();
            time_elapsed = 0;
        }
    }

    private void SpawnEnemies()
    {
        int current_enemies = 0;
        for (int i = 0; i < spawn_points.Length; i++)
        {
            current_enemies += spawn_points[i].childCount;
        }

        int rand = Random.Range(2, 5);
        if (current_enemies + rand <= max_enemies)
        {
            for (int i = 0; i < rand; i++)
            {
                int rand_loc = Random.Range(0, spawn_points.Length);
                GameObject enemy = Instantiate(enemy_prefab, spawn_points[rand_loc].position, Quaternion.identity, spawn_points[rand_loc]);
            }
        }
    }
}
