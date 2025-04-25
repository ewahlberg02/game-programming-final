using UnityEngine;

public class Axe : Weapon
{
    private float cooldown = 0f;

    void Start()
    {
        level = 1;
        damage = 30;
        fireRate = 2f;
        projSpeed = 1f;
        projLifetime = 5f;
        affectedGravity = true;
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

        Weapon_Projectile proj = Instantiate(projPrefab);
        proj.Damage = damage;
        proj.Speed = projSpeed;
        proj.Lifetime = projLifetime;
        proj.affectedGravity = affectedGravity;
        proj.visualSprite = sprite;

        proj.Direction = Vector3.up + new Vector3(Random.Range(-2f,2f), 0);

        projectiles.Add(proj);
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 5 * level;
        fireRate = Mathf.Clamp(fireRate * 0.95f, 0.05f, 10);
        projSpeed *= 1.15f;
        projLifetime *= 1.1f;
    }
}
