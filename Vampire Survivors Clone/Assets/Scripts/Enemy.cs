using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] int max_health = 10;

    [SerializeField] int health = 1;
    [SerializeField] int damage = 1;

    [SerializeField] float speed = 1.0f;
    [SerializeField] float size = 0.25f;
    [SerializeField] float attack_range = 0.3f;    
    [SerializeField] float attack_cooldown = 0.5f;
    private bool can_attack = true;

    private new SpriteRenderer renderer;
    private bool damage_indicator = false;


    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = gameObject.GetComponent<SpriteRenderer>();

        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target_position = player.transform.position;
        float dist_away = Vector3.Distance(transform.position, target_position);

        if (dist_away > attack_range) {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target_position, step);
        }
        
        if (can_attack && dist_away <= attack_range) {
            StartCoroutine(AttackPlayer());
        }
    }

    public void SetStats(int _level, int _max_health, int _damage, float _speed, float _size = 0.25f) {
        level = _level;
        max_health = _max_health;
        damage = _damage;
        speed = _speed;
        size =_size;
        AmplifyStatsByLevel();
    }

    private void AmplifyStatsByLevel() {
        float amplify = (float)Math.Pow(1.05, level - 1);

        max_health = (int)(max_health * amplify);
        damage = (int)(damage * amplify);
        speed *= amplify;
        size *= amplify;
        attack_range = size + 0.02f;

        health = max_health;
    }

    private IEnumerator AttackPlayer() {
        can_attack = false;

        yield return new WaitForSeconds(attack_cooldown);
        can_attack = true;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            StartCoroutine(Die());
        }
        else if (!damage_indicator) {
            StartCoroutine(DamagedIndication());
        }
    }

    private IEnumerator DamagedIndication() {
        damage_indicator = true;
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        renderer.color = Color.white;
        damage_indicator = false;
    }

    private IEnumerator Die() {
        speed = 0.0f;
        GetComponent<BoxCollider2D>().enabled = false;
        renderer.color = Color.gray;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++) {
            renderer.color = Color.Lerp(renderer.color, Color.clear, i/10.0f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered me");
    }
}
