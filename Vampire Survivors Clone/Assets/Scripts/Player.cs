using UnityEngine;

public class Player : MonoBehaviour
{

    int player_current_health;
    int player_base_max_health;
    int player_base_speed = 1;
    int player_base_defense;

    void Update()
    {
        player_movement();
    }

    void player_movement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 playerMoveVector = new Vector3(moveX, moveY, 0);

        transform.position += playerMoveVector * Time.deltaTime * player_base_speed;
    }
}
