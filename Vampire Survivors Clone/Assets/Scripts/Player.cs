using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int player_current_health;
    public int player_max_health;
    public float player_speed = 1f;
    public int player_defense;
    public int player_attack;
    public float player_heal_modifier;
    public int player_xp;
    public int player_level;
    public float pickup_range;
    [SerializeField] levelUp levelUpScreen;
    [SerializeField] xpBarScript XPBar;
    [SerializeField] HealthBar hpBar;
    [SerializeField] GameOver dead;
    public float xp_need;
    private AudioSource hitSound;

    void Start()
    {
        initialize_stats();
        hitSound = GetComponent<AudioSource>();     
    }

    void Update()
    {
        xp_need = xp_to_level();
        player_movement();
    }

    void player_movement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 playerMoveVector = new Vector3(moveX, moveY, 0);

        transform.position += playerMoveVector * Time.deltaTime * player_speed;
    }

    public void initialize_stats(){
        player_max_health = 10;
        player_current_health = player_max_health;
        player_defense = 1;
        player_speed = 1;
        player_attack = 1;
        player_heal_modifier = 1.0f;
        pickup_range = 0.75f;
        player_level = 1;
        hpBar.SetMaxHealth();
    }

    public void take_damage(int damage){
        damage -= player_defense;
        hitSound.Play();
        Mathf.Clamp(damage, 0, 5000);
        player_current_health -= damage;
        player_current_health = Mathf.Clamp(player_current_health, 0, player_max_health);
        hpBar.SetCurrentHealth();
        if(player_current_health == 0)
        {
            dead.endGame();
        }
    }

    public void heal(float heal_amount){
        heal_amount *= player_heal_modifier;
        Mathf.Round(heal_amount);
        int heal = Convert.ToInt32(heal_amount);
        player_current_health += heal;
        player_current_health = Mathf.Clamp(player_current_health, 0, player_max_health);
        hpBar.SetCurrentHealth();
    }

    public void stat_increase(string stat){
        int increase_value_int = (player_level / 10) + 1;
        float increase_value_float = (float)player_level / 10;
        switch (stat){
            case "attack":
                player_attack += increase_value_int * 3;
                break;
            case "speed":
                player_speed += increase_value_float/10f;
                break;
            case "hp":
                player_max_health += increase_value_int;
                player_current_health += increase_value_int;
                break;
            case "defense":
                player_defense += increase_value_int;
                break;
            case "pickup":
                pickup_range += increase_value_float;
                break;
            case "heal_mod":
                player_heal_modifier += increase_value_float;
                break;
        }
    }

    public void add_xp(int xp_value){
        player_xp += xp_value;
        if (player_xp >= xp_to_level()){
            level_up();
        }
        XPBar.UpdateInterface();
    }

    public void level_up(){ // Call if player_xp > xp_needed
        int xp_needed = (int)xp_to_level();
        player_xp -= xp_needed;
        player_level++;
        levelUpScreen.gameObject.SetActive(true);
        levelUpScreen.initialize();
        
    }

    public float xp_to_level(){
        float xp_needed = Mathf.Pow(player_level, 2); 
        return xp_needed;
    }
}
