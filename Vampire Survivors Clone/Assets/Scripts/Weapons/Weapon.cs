using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    [SerializeField]protected Sprite sprite;
    [SerializeField]protected Weapon_Projectile projPrefab;
    [SerializeField]protected int weaponId;

    protected Player player;
    protected int level = 1;
    protected int damage = 1;
    protected float fireRate = 1f;
    protected float projSpeed = 1f;
    protected float projLifetime = 1f;
    protected bool canFire = true;
    protected bool affectedGravity = false;

    protected List<Weapon_Projectile> projectiles = new List<Weapon_Projectile>();

    public bool CanFire {
        get {return canFire;}
    }

    public int WeaponId {
        get {return weaponId; }
    }

    abstract public void fire();

    abstract public void levelIncrease();
}
