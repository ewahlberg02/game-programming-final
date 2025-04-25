using UnityEngine;

public class WeaponChest : MonoBehaviour
{
    [SerializeField] Weapon[] availableWeapons;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Player"){
            WeaponController controller = collidedObject.GetComponent<WeaponController>();
            if(!controller) return;

            int randomIndex = Random.Range(0, availableWeapons.Length);
            Weapon createdWeapon = Instantiate(availableWeapons[randomIndex]);
            controller.AddWeapon(createdWeapon);
        }
    }
}
