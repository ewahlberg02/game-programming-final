using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventroyController : MonoBehaviour
{
   
    private Dictionary<int, GameObject> itemDictionary = new Dictionary<int, GameObject>();


    [SerializeField] private WeaponController controller;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject[] itemPrefabs; // Indexed by weapon ID or similar

 

    void Start()
    {

    }

    public void updateInventory(Weapon weapon)
    {
        int weaponid = weapon.WeaponId - 1;

        SlotScript slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<SlotScript>();
        GameObject item = Instantiate(itemPrefabs[weaponid], slot.transform);

        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        slot.currentItem = item;
      
        itemDictionary[weaponid] = item;
    }

    public void LevelUpWeapon(Weapon w)
    {
        int weaponid = w.WeaponId - 1;
        TextMeshProUGUI ItemLevelText = itemDictionary[weaponid].GetComponentInChildren<TextMeshProUGUI>();
        ItemLevelText.text = $"{w.Level}";
    }


}