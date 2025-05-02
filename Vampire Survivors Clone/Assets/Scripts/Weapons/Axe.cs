using System;
using Unity.Mathematics;
using UnityEngine;

public class Axe : Weapon
{
    private float cooldown = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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

        Weapon_Projectile proj = Instantiate(projPrefab, transform.position, quaternion.identity);
        proj.Direction = Vector3.up + new Vector3(UnityEngine.Random.Range(-2f,2f), 0);
        
        proj.Damage = damage + player.player_attack;
        proj.Speed = projSpeed;
        proj.Lifetime = projLifetime;
        proj.affectedGravity = affectedGravity;
        proj.visualSprite = sprite;

        projectiles.Add(proj);
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 5 * level;
        fireRate = Mathf.Clamp(fireRate * 0.95f, 0.05f, 10);
        projSpeed = Mathf.Clamp(projSpeed * 1.08f, 0.5f, 4f);
        projLifetime *= 1.1f;
    }
}
