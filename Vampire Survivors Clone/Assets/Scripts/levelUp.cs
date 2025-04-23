using UnityEngine;
using UnityEngine.EventSystems;

public class levelUp : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject[] optionArray;

    void Start()
    {
        initialize();
    }

    public void initialize()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void OnClick()
    {
        string button_name = EventSystem.current.currentSelectedGameObject.name;
        player.stat_increase(button_name);
    }

}
