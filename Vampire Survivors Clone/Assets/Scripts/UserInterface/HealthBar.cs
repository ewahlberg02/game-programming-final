using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image HealthFill;

    public void SetMaxHealth()
    {
        HealthFill.fillAmount = 1;
    }

    public void SetCurrentHealth()
    {
        float start = player.player_current_health;
        float end = player.player_max_health;
        HealthFill.fillAmount = (float)start / (float)end;
    }
}

