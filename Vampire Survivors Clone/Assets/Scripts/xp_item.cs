using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class xp_item : MonoBehaviour
{
    [SerializeField] int xp_value;
    [SerializeField] int speed = 3;
    private AudioSource itemAudio;
    bool canDelete;

    void Start()
    {
        itemAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        move_to_player();
        canDelete = !itemAudio.isPlaying;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player"){
            Player player = collidedObject.GetComponent<Player>();
            player.add_xp(xp_value);
            itemAudio.Play();
            StartCoroutine(destroyXP());
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

    IEnumerator destroyXP(){
        yield return new WaitUntil(()  => canDelete);
        Destroy(gameObject);
    }
}
