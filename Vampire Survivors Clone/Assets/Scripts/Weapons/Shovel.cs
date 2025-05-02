using Unity.Mathematics;
using UnityEngine;

public class Shovel : Weapon
{
    private float cooldown = 0f;
    private float quickCastChance = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        level = 1;
        damage = 15;
        fireRate = 1f;
        projSpeed = 2f;
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

        Weapon_Projectile proj = Instantiate(projPrefab, transform.position, quaternion.identity);
        proj.transform.position += new Vector3(0, UnityEngine.Random.Range(-.05f, .05f));
        proj.Direction = Vector3.left;
        if (UnityEngine.Random.Range(0f, 1f) > 0.5f) {
            proj.Direction = Vector3.right;
        }

        proj.Damage = damage + player.player_attack;
        proj.Speed = projSpeed;
        proj.Lifetime = projLifetime;
        proj.affectedGravity = affectedGravity;
        proj.visualSprite = sprite;

        projectiles.Add(proj);

        RollQuickCast();
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 10;
        fireRate = Mathf.Clamp(fireRate * 0.95f, 0.2f, 10);
        projSpeed = Mathf.Clamp(projSpeed * 1.10f, 0.05f, 7.5f);
        projLifetime *= 0.96f;
        quickCastChance = Mathf.Clamp(level * 0.03f, 0f, .7f);
    }

    private void RollQuickCast() {
        if (UnityEngine.Random.Range(0f, 1f) < quickCastChance) {
            cooldown = 0.05f;
        }
    }
}
