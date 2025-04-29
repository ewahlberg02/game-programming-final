using Unity.Mathematics;
using UnityEngine;

public class Dagger : Weapon
{
    private float cooldown = 0f;

    void Start()
    {
        level = 1;
        damage = 5;
        fireRate = 0.5f;
        projSpeed = 3f;
        projLifetime = 1f;
    }

    void Update()
    {
        canFire = cooldown <= 0.0f;
        cooldown -= Time.deltaTime;

        if (canFire)
            fire();
    }

    public override void fire()
    {
        cooldown = fireRate;
        canFire = false;

        Weapon_Projectile proj = Instantiate(projPrefab, transform.position, quaternion.identity);
        proj.Direction = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0f);
        
        proj.Damage = damage;
        proj.Speed = projSpeed;
        proj.Lifetime = projLifetime;
        proj.affectedGravity = affectedGravity;
        proj.visualSprite = sprite;

        projectiles.Add(proj);
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 7;
        fireRate = Mathf.Clamp(fireRate * 0.90f, 0.05f, 10);
        projSpeed *= 1.05f;
        projLifetime *= 1.1f;
    }
}
