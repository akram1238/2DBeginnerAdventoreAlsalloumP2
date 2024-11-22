using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControler : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float TimeInvincible = 2.0f;
    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("look X", lookDirection.x);
        animator.SetFloat("look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit2D.collider != null)
            {
                Debug.Log("Raycast has hit the object" + hit2D.collider.gameObject);
            }
        
                 void FixedUpdate()
            {
                Vector2 position = rigidbody2d.position;
                position.x = position.x + speed * horizontal * Time.deltaTime;
                position.y = position.y + speed * vertical * Time.deltaTime;
                rigidbody2d.MovePosition(position);
            }
             void ChangeHealth(int amount)
            {
                if (amount < 0)
                {
                    animator.SetTrigger("Hit");

                    if (isInvincible)
                    {
                        return;
                    }
                    isInvincible = true;
                    invincibleTimer = TimeInvincible;
                }
                currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
                UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            }
        }
    }
}
