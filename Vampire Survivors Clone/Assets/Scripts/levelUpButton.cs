using UnityEngine;

public class levelUpButton : MonoBehaviour
{
    [SerializeField] string stat;

    Player player;
    levelUp levelUp;

    void Start(){
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        levelUp = FindAnyObjectByType<levelUp>();
    }

    public void OnClick(){
        if(player != null){
            player.stat_increase(stat);
        }
        if(levelUp != null){
            levelUp.clearScreen();
        }
    }
}
