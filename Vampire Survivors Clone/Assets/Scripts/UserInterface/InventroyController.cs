using UnityEngine;
using TMPro;

public class InventroyController : MonoBehaviour
{
    [SerializeField] private WeaponController controller;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject[] itemPrefabs; // Indexed by weapon ID or similar
    [SerializeField] TextMeshProUGUI ItemLevelText;


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
    }

   
    

}