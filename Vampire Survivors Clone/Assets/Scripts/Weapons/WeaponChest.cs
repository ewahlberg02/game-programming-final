using UnityEngine;

public class WeaponChest : MonoBehaviour
{
    [SerializeField] Weapon[] availableWeapons;
    public ChestManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player"){
            WeaponController controller = collidedObject.GetComponentInChildren<WeaponController>();
            if(!controller) return;

            int randomIndex = Random.Range(0, availableWeapons.Length);
            Weapon createdWeapon = availableWeapons[randomIndex];
            if (!controller.HoldingWeaponId(createdWeapon.WeaponId)) {
                Weapon weapon = Instantiate(createdWeapon);
                controller.AddWeapon(weapon);
            }
            else {
                controller.LevelWeaponWithId(createdWeapon.WeaponId);
            }
            manager.RemoveChest(this);
            Destroy(gameObject);
        }
    }
}
