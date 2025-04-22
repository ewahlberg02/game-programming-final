using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{

    int player_current_health;
    int player_max_health;
    int player_speed = 1;
    int player_defense;
    int player_attack;
    float player_heal_modifier = 1.0f;
    public float player_xp;
    public int player_level;
    public float pickup_range = 0.75f;

    void Update()
    {
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
    }

    public void take_damage(int damage){
        damage -= player_defense;
        Mathf.Clamp(damage, 0, 5000);
        player_current_health -= damage;
    }

    public void heal(float heal_amount){
        heal_amount *= player_heal_modifier;
        Mathf.Round(heal_amount);
        int heal = Convert.ToInt32(heal_amount);
        player_current_health += heal;
    }

    public void stat_increase(){
        return;
    }

    public void add_xp(int xp_value){
        player_xp += xp_value;
        if (player_xp >= xp_to_level()){
            level_up();
        }
    }

    void level_up(){ // Call if player_xp > xp_needed
        player_xp -= xp_to_level();
        player_level++;
        return;
    }

    private float xp_to_level(){
        float xp_needed = Mathf.Pow(player_level, 2); 
        return xp_needed;
    }
}
