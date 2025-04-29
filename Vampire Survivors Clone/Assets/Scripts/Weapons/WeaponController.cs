using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();

    public void Update()
    {
        


        foreach (Weapon weapon in weapons) {
            if(weapon.CanFire) {
                weapon.fire();
            }
        }
    }

    public bool HoldingWeaponId(int id) {
        foreach (Weapon w in weapons) {
            if (w.WeaponId == id)
                return true;
        }
        return false;
    }

    public void LevelWeaponWithId(int id) {
        foreach (Weapon w in weapons) {
            if (w.WeaponId == id) {
                w.levelIncrease();
                return;
            }
        }
    }

    public void AddWeapon(Weapon weapon) {
        weapons.Add(weapon);
        weapon.transform.parent = gameObject.transform;
        weapon.transform.position = gameObject.transform.position;
    }
}
