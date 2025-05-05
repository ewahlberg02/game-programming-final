using Unity.Mathematics;
using UnityEngine;

public class FlameStep : Weapon
{
    private float cooldown = 0f;
    private AudioSource itemAudio;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        itemAudio = GetComponent<AudioSource>();
        level = 1;
        damage = 3;
        fireRate = 0.25f;
        projSpeed = 0f;
        projLifetime = 2f;
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
        itemAudio.Play();

        proj.Direction = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0f);
        
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
        damage += 3;
        fireRate = Mathf.Clamp(fireRate * 0.98f, 0.1f, 10);
        projLifetime += 0.3f;
    }
}
