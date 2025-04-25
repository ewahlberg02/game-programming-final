using UnityEngine;

public class Shovel : Weapon
{
    private float cooldown = 0f;
    private float quickCastChance = 0.8f;

    void Start()
    {
        level = 1;
        damage = 15;
        fireRate = 1f;
        projSpeed = 3.5f;
        projLifetime = 0.25f;
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

        proj.Direction = Vector3.left;
        if (Random.Range(0f, 1f) > 0.5f) {
            proj.Direction = Vector3.right;
        }

        proj.transform.position += new Vector3(0, Random.Range(-.05f, .05f));

        projectiles.Add(proj);

        RollQuickCast();
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 10;
        fireRate = Mathf.Clamp(fireRate * 0.90f, 0.05f, 10);
        projSpeed *= 1.10f;
        projLifetime *= 0.95f;
        quickCastChance = Mathf.Clamp(level * 0.03f, 0f, .7f);
    }

    private void RollQuickCast() {
        if (Random.Range(0f, 1f) < quickCastChance) {
            cooldown = 0.05f;
        }
    }
}
