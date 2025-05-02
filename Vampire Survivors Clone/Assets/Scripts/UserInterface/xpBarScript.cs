using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class xpBarScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Player player;
    [SerializeField] Image experienceFill;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInterface()
    {
        float start = player.player_xp;
        float end = player.xp_need;
        levelText.text = player.player_level.ToString();
        experienceFill.fillAmount = (float)start / (float)end;
    }
}
