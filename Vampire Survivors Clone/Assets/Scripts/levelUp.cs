using UnityEngine;
using UnityEngine.EventSystems;

public class levelUp : MonoBehaviour
{
    [SerializeField] GameObject[] optionArray;
    [SerializeField] Canvas canvas;
    [SerializeField] Player player;

    public void initialize()
    {
        int randomStat = Random.Range(0, optionArray.Length);
        int firstStat = randomStat;
        GameObject buttonInstance = Instantiate(optionArray[randomStat]);
        buttonInstance.transform.SetParent(canvas.transform, true);
        buttonInstance.transform.position = new Vector3(Screen.width / 2 - 100, Screen.height / 2, 0);

        randomStat = Random.Range(0, optionArray.Length);
        while (randomStat == firstStat){
            randomStat = Random.Range(0, optionArray.Length);
        }
        buttonInstance = Instantiate(optionArray[randomStat]);
        buttonInstance.transform.SetParent(canvas.transform, true);
        buttonInstance.transform.position = new Vector3(Screen.width / 2 + 100, Screen.height / 2, 0);
        Time.timeScale = 0;
    }

    public void clearScreen(){
        Destroy(canvas.transform.GetChild(0).gameObject);
        Destroy(canvas.transform.GetChild(1).gameObject);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (player.player_xp > player.xp_to_level()){
            player.level_up();
        }
    }
}
