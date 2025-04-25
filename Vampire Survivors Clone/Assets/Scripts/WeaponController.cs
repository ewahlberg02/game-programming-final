using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    List<Weapon> weapons = new List<Weapon>();

    public void Update()
    {
        foreach (Weapon weapon in weapons) {
            if(weapon.CanFire) {
                weapon.fire();
            }
        }
    }

    public void AddWeapon(Weapon weapon) {
        // if weapon is already obtained, level it up.
        foreach (Weapon w in weapons) {
            if (w.WeaponId == weapon.WeaponId) {
                w.levelIncrease();
                return;
            }
        }

        // otherwise add it
        weapons.Add(weapon);
    }
}
