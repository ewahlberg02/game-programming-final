using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChestManager : MonoBehaviour
{

    [SerializeField] float chance_any = 0.8f;
    [SerializeField] float dist_away = 2.5f;
    [SerializeField] int max_chests = 5;
    [SerializeField] float chest_interval;
    [SerializeField] WeaponChest[] availableChests;

    float last_chest_spawn = 0;

    [SerializeField] Sprite sprite;
    GameObject indicator;
    new SpriteRenderer renderer;

    Player player;
    public List<WeaponChest> chests;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chests = new List<WeaponChest>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        indicator = new GameObject();
        renderer = indicator.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        SpawnChest();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > chest_interval + last_chest_spawn && chests.Count < max_chests) {
            SpawnChest();
        }
        if (chests.Count > 0) {
            IndicateNearestChest();
        }
        else {
            renderer.color = Color.clear;
        }
    }


    private void IndicateNearestChest()
    {
        Vector3 nearestPosition = chests[0].transform.position;
        Vector3 playerPos = player.transform.position;
        float nearestDist = Vector3.Distance(playerPos, nearestPosition);
        foreach (var chest in chests) {
            float dist = Vector3.Distance(playerPos, chest.transform.position);
            if (dist < nearestDist) {
                nearestDist = dist;
                nearestPosition = chest.transform.position;
            }
        }
        //Debug.Log("Nearest at " + nearestPosition);
        indicator.transform.position = Vector3.MoveTowards(playerPos, nearestPosition, 0.75f);
        //float angle = Vector3.Angle(playerPos, nearestPosition);
        //Vector3 direction = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0f);
        //indicator.transform.eulerAngles = new Vector3(0f, 0f, (float)(Math.Atan2(direction.y, direction.x) * 180.0f / Math.PI) - 90f);
        Quaternion rotation = Quaternion.LookRotation(
            nearestPosition - playerPos ,
            transform.TransformDirection(Vector3.up)
        );
        indicator.transform.rotation = new Quaternion( 0 , 0 , rotation.z, rotation.w );
        renderer.color = Color.white;
    }

    private void SpawnChest() {
        Vector3 spawn_position = Vector3.zero;
        bool valid_spawn = false;
        for (int i=0; i<100; i++) {
            spawn_position = RandomSpawnPoint();
            if (!Physics2D.OverlapCircle(spawn_position, 0.3f)) {
                valid_spawn = true;
                break;
            }
        }
        if (!valid_spawn) {
            Debug.Log("No valid spots to spawn chest");
            return;
        }

        WeaponChest chest_to_spawn;
        float roll = UnityEngine.Random.Range(0.0f, 1.0f);
        if (roll < chance_any) {
            chest_to_spawn = Instantiate(availableChests[0], spawn_position, quaternion.identity);
            chest_to_spawn.manager = this;
            chests.Add(chest_to_spawn);
        }
        else {
            int index = UnityEngine.Random.Range(1, availableChests.Length);
            chest_to_spawn = Instantiate(availableChests[index], spawn_position, quaternion.identity);
            chest_to_spawn.manager = this;
            chests.Add(chest_to_spawn);
        }
        last_chest_spawn = Time.timeSinceLevelLoad;
    }

    private Vector3 RandomSpawnPoint() {
        float angle = UnityEngine.Random.Range(0, 2 * (float)Mathf.PI);
        float x = player.transform.position.x + dist_away * Mathf.Cos(angle);
        float y = player.transform.position.y + dist_away * Mathf.Sin(angle);
        return new Vector3(x, y, 0f);
    }

    public void RemoveChest(WeaponChest chest) {
        chests.Remove(chest);
    }
}
