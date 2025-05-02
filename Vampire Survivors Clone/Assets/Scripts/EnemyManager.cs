using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Sprite[] enemySprites;
    [SerializeField] Sprite[] advEnemySprites;

    [SerializeField] int points = 0;
    [SerializeField] float point_acrue_interval = 0.1f;
    private float last_acrue_time = 0f;

    [SerializeField] float min_spawn_interval = 0.66f;
    private float last_spawn_time = 0f;

    [SerializeField] float min_heavy_spawn_interval = 3.5f;
    private float last_heavy_spawn_time = 0f;

    [SerializeField] float min_adv_spawn_interval = 15f;
    private float last_adv_spawn_time = 0f;

    [SerializeField] float stat_increm_interval = 8f;
    private float last_stat_increm = 0f;

    [SerializeField] int base_max_health = 25;
    [SerializeField] int base_damage = 4;
    [SerializeField] float base_speed = 0.5f;
    [SerializeField] float base_size = 1f;

    [SerializeField] int increm_health = 5;
    [SerializeField] int increm_damage = 1;
    [SerializeField] float increm_speed = 0.002f;

    private GameObject player;
    [SerializeField] float dist_away = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > last_acrue_time + point_acrue_interval) {
            last_acrue_time = Time.timeSinceLevelLoad;
            points += 1;
        }

        if(Time.timeSinceLevelLoad > last_stat_increm + stat_increm_interval) {
            IncrementStats();
        }

        DecideSpawn();
    }

    private void IncrementStats()
    {
        last_stat_increm = Time.timeSinceLevelLoad;
        base_max_health += increm_health;
        base_damage += increm_damage;
        base_speed = Mathf.Clamp(base_speed + increm_speed, 0.2f, 2f);
    }

    private void DecideSpawn() {
        if (Time.timeSinceLevelLoad < last_spawn_time + min_spawn_interval) {
            DecideSpecialSpawn();
            return;
        }

        if(points < 100) {
            BasicSpawn();
        }
        else if (points < 250) {
            BasicSpawn();
            if (Time.timeSinceLevelLoad > last_heavy_spawn_time + min_heavy_spawn_interval) {
                HeavySpawn();
            }
        }
        else {
            if (Time.timeSinceLevelLoad > last_adv_spawn_time + min_adv_spawn_interval) {
                AdvancedSpawn();
            }
        }

        last_spawn_time = Time.timeSinceLevelLoad;
    }

    private void DecideSpecialSpawn() {

    }

    private void BasicSpawn() {
        Enemy enemy = Instantiate(enemyPrefab, RandomSpawnPoint(), quaternion.identity);
        int level = UnityEngine.Random.Range(1, 4);
        enemy.GetComponent<Enemy>().SetStats(level, base_max_health, base_damage, base_speed, base_size);
        enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[UnityEngine.Random.Range(0, enemySprites.Length)];
        points -= level;
    }

    private void HeavySpawn() {
        last_heavy_spawn_time = Time.timeSinceLevelLoad;
        int spawns = UnityEngine.Random.Range(2,4);

        for (int i = 0; i < spawns; i++) {
            Enemy enemy = Instantiate(enemyPrefab, RandomSpawnPoint(), quaternion.identity);
            int level = UnityEngine.Random.Range(4, 11);
            int health_bump = level * 5;
            int damage_bump = level * 2;
            float speed_bump = -0.05f;
            float size_for_heavy = base_size + 0.3f;
            enemy.GetComponent<Enemy>().SetStats(level, base_max_health + health_bump, base_damage + damage_bump, base_speed + speed_bump, size_for_heavy);
            enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[UnityEngine.Random.Range(0, enemySprites.Length)];
            points -= level;
        }
    }

    private void AdvancedSpawn() {
        last_adv_spawn_time = Time.timeSinceLevelLoad;
        int chosen_sprite_index = UnityEngine.Random.Range(0, advEnemySprites.Length);

        while (points > 20) {
            Enemy enemy = Instantiate(enemyPrefab, RandomSpawnPoint(), quaternion.identity);
            int level = UnityEngine.Random.Range(8, 16);
            int health_bump = level * 10;
            int damage_bump = level * 3;
            float speed_bump = -0.1f;
            float size_for_adv = base_size + 0.8f;
            enemy.GetComponent<Enemy>().SetStats(level, base_max_health + health_bump, base_damage + damage_bump, base_speed + speed_bump, size_for_adv);
            enemy.GetComponent<SpriteRenderer>().sprite = advEnemySprites[chosen_sprite_index];
            points -= level * 5;
        }
        while(points > 0) {
            BasicSpawn();
        }

        points = 0;
    }

    private Vector3 RandomSpawnPoint() {
        float angle = UnityEngine.Random.Range(0, 2 * (float)Math.PI);
        float x = player.transform.position.x + dist_away * Mathf.Cos(angle);
        float y = player.transform.position.y + dist_away * Mathf.Sin(angle);
        return new Vector3(x, y, 0f);
    }
}
