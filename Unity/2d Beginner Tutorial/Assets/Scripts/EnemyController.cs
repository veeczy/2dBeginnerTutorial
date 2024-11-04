using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //movement
    public bool vertical;
    public float speed = 3f;
    Rigidbody2D rigidbody2d;

    //behavior
    bool aggressive = true;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    //animations
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = changeTime;
        rigidbody2d = GetComponent < Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }
    void FixedUpdate()
    {
        if(!aggressive)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
