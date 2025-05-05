using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BowArrow : Weapon
{
    private float cooldown = 0f;
    private int numArrows = 3;
    private AudioSource itemAudio;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        itemAudio = GetComponent<AudioSource>();
        level = 1;
        damage = 8;
        fireRate = 3f;
        projSpeed = 2.5f;
        projLifetime = 1.5f;
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

        StartCoroutine(fireArrow());
    }

    private IEnumerator fireArrow() {
        for (int i = 0; i < numArrows; i++) {
            Weapon_Projectile proj = Instantiate(projPrefab, transform.position, quaternion.identity);
            itemAudio.Play();
            double angle = i * (Math.PI * 2 / (double)numArrows);
            proj.Direction = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0f);
            
            proj.Damage = damage + player.player_attack;
            proj.Speed = projSpeed;
            proj.Lifetime = projLifetime;
            proj.affectedGravity = affectedGravity;
            proj.visualSprite = sprite;

            projectiles.Add(proj);
            yield return new WaitForSeconds( 0.5f / numArrows);
        }
    }

    public override void levelIncrease()
    {
        level += 1;
        damage += 3;
        fireRate = Mathf.Clamp(fireRate * 0.95f, 0.05f, 10);
        projSpeed *= 1.05f;
        projLifetime *= 1.1f;
        numArrows += 1;
    }
}
