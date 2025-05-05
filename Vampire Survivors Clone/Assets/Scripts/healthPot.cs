using UnityEngine;

public class healthPot : MonoBehaviour
{
    [SerializeField] int healAmount;
    int speed = 1;

    void Update()
    {
        move_to_player();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player"){
            Player player = collidedObject.GetComponent<Player>();
            player.heal(healAmount);
            Destroy(gameObject);
        }
    }

    void move_to_player(){
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Vector3 player_position = player.transform.position;
        Vector3 item_position = gameObject.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(item_position.x - player_position.x, 2) + Mathf.Pow(item_position.y - player_position.y, 2));
        Vector3 moveVector = player_position - item_position;

        if (distance <= player.pickup_range){
            gameObject.transform.position += moveVector * Time.deltaTime * speed;
        }
    }
}
