using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Weapon_Projectile : MonoBehaviour
{
    private int damage = 1;
    private float speed = 1;
    private float lifetime = 3f;
    private Vector3 direction;

    public bool affectedGravity = false;
    public Sprite visualSprite;

    public int Damage {
        set { damage = Math.Clamp(value, 0, 10000); }
        get { return damage; }
    }

    public float Speed {
        set { speed = Math.Clamp(value, 0, 50); }
        get { return speed; }
    }

    public float Lifetime {
        set { lifetime = (float)Math.Clamp(value, 0.1, 30f); }
        get { return lifetime; }
    }

    public Vector3 Direction {
        set {
            direction = value;
            direction.Normalize();
        }
        get {return direction;}
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer) {
            renderer.sprite = visualSprite;
        }
        
        transform.Rotate(0f, 0f, (float)(Math.Atan2(direction.y, direction.x) * 180.0f / Math.PI) - 90f);
        StartCoroutine(DieAfterLifetime());
    }

    private IEnumerator DieAfterLifetime() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);

        if (affectedGravity) {
            direction.y -= 1f * Time.deltaTime;
            direction.Normalize();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        var collidedObject = collision.gameObject;
        if (collidedObject.tag == "Enemy") {
            collidedObject.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
