using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Projectile collision with " + other.gameObject);
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.Fix();
        }
        Destroy(gameObject);
    }
}
