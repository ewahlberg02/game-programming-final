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
    [SerializeField] float attack_windup = 0.15f;
    [SerializeField] xp_item small_xp;
    [SerializeField] xp_item med_xp;
    [SerializeField] xp_item large_xp;
    [SerializeField] xp_item xl_xp;
    [SerializeField] DamagePopup popup;
    [SerializeField] healthPot small_heal;
    [SerializeField] healthPot med_heal;
    [SerializeField] healthPot large_heal;
    [SerializeField] healthPot xl_heal;

    private bool can_attack = true;

    private new SpriteRenderer renderer;
    private bool damage_indicator = false;
    private AudioSource hitSound;


    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = gameObject.GetComponent<SpriteRenderer>();
        hitSound = GetComponent<AudioSource>();

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

        if (dist_away > 5.0f) {
            Destroy(gameObject);
        }
    }

    public void SetStats(int _level, int _max_health, int _damage, float _speed, float _size) {
        level = _level;
        max_health = _max_health;
        damage = _damage;
        speed = _speed;
        size =_size;

        AmplifyStatsByLevel();
        health = max_health;
        gameObject.transform.localScale = new Vector3(size, size, 0f);
    }

    private void AmplifyStatsByLevel() {
        float amplify = (float)Math.Pow(1.05, level - 1);

        max_health = (int)(max_health * amplify);
        damage = (int)(damage * amplify);
        speed *= amplify;
        size *= amplify;
        attack_range = size / 7.0f;
    }

    private IEnumerator AttackPlayer() {
        can_attack = false;
        yield return new WaitForSeconds(attack_windup);

        Vector3 target_position = player.transform.position;
        float dist_away = Vector3.Distance(transform.position, target_position);
        if (dist_away <= attack_range) {
            player.SendMessage("take_damage", damage);
        }

        yield return new WaitForSeconds(attack_cooldown);
        can_attack = true;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        hitSound.Play();
        createDamagePopup(amount);
        if (health <= 0) {
            StartCoroutine(Die());
        }
        else if (!damage_indicator) {
            StartCoroutine(DamagedIndication());
        }
    }

    private void createDamagePopup(int damage){
        DamagePopup newPopup = Instantiate(popup);
        newPopup.initialize(damage);
        newPopup.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
        
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
        spawnXP();
        spawnHP();
        Destroy(gameObject);
    }

    private void spawnXP(){
        float timeInLevel = Time.timeSinceLevelLoad;
        if (timeInLevel < 180f){
            xp_item xp = Instantiate(small_xp, transform.position, transform.rotation);
            Debug.Log("xp dropped");
        } else if (timeInLevel > 180.01f && timeInLevel < 300f){
            xp_item xp = Instantiate(med_xp, transform.position, transform.rotation);
        } else if (timeInLevel > 300.01f  && timeInLevel < 1000f){
            xp_item xp = Instantiate(large_xp, transform.position, transform.rotation);
        } else if (timeInLevel > 1000.01f){
            xp_item xp = Instantiate(xl_xp, transform.position, transform.rotation);
        }
    }
    private void spawnHP(){
        float timeInLevel = Time.timeSinceLevelLoad;
        float chance = UnityEngine.Random.Range(0, 1);
        if (chance <= 0.1){
            if (timeInLevel < 180f){
                healthPot pot = Instantiate(small_heal, transform.position, transform.rotation);
                Debug.Log("xp dropped");
            } else if (timeInLevel > 180.01f && timeInLevel < 300f){
                healthPot pot = Instantiate(med_heal, transform.position, transform.rotation);
            } else if (timeInLevel > 300.01f  && timeInLevel < 1000f){
                healthPot pot = Instantiate(large_heal, transform.position, transform.rotation);
            } else if (timeInLevel > 1000.01f){
                healthPot pot = Instantiate(xl_heal, transform.position, transform.rotation);
            }
        }
    }
}
