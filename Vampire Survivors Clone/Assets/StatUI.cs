using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MaxHealthValueText;
    [SerializeField] TextMeshProUGUI SpeedValueText;
    [SerializeField] TextMeshProUGUI DefenseValueText;
    [SerializeField] TextMeshProUGUI AttackValueText;
    [SerializeField] TextMeshProUGUI HealModValueText;
    [SerializeField] TextMeshProUGUI PickupRangeValueText;
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        float percentValue = player.player_heal_modifier * 100;
        MaxHealthValueText.text = player.player_max_health.ToString();
        SpeedValueText.text = player.player_speed.ToString();
        DefenseValueText.text = player.player_defense.ToString();
        AttackValueText.text = player.player_attack.ToString();
        HealModValueText.text = percentValue.ToString() + "%";
        PickupRangeValueText.text = player.pickup_range.ToString();
    }
}
