using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rend;

    public bool climb;
    bool jump;

    private bool canGrab;
    public bool grabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.dKey.isPressed)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        else if(Keyboard.current.wKey.isPressed && climb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
        else if(Keyboard.current.sKey.isPressed && climb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if(Keyboard.current.spaceKey.isPressed && canGrab)
        {
            grabbed=true;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Ground"))
        {
            jump = true;
        }
    }


    private void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Rope"))
        {
            climb = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0, 0);
        }
        if (c.CompareTag("BoxRope"))
        {
            canGrab = true;   
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Rope"))
        {
            climb = false;
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 1f;
        }

        if (c.CompareTag("BoxRope"))
        {
            canGrab=false;
        }
    }

}
