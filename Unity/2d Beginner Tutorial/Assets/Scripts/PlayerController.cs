using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    //health
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    //invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    //animations
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    //attack
    public GameObject projectilePrefab;
    public InputAction launchAction;

    // Start is called before the first frame update
    void Start()
    {
        //movement
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        //call health
        currentHealth = maxHealth;

        //animations
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //movement
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);

        //animations
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(0.0f, move.y))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //invince
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if(damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        //attack
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth (int amount)
    {
        
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        //Debug.Log(currentHealth + "/" + maxHealth);
    }

    //attack function
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }
}
