using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rend;
<<<<<<< HEAD:Assets/_Scripts/PlayerControl.cs
    bool climb;
=======

    public bool climb;
>>>>>>> f68fdc1ec05d82c467a40d9da0e20457f9c21eee:Assets/Scripts/PlayerControl.cs
    bool jump;


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
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Ground") || c.collider.CompareTag("Boulder"))
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
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("Rope"))
        {
            climb = false;
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 1f;
        }
    }

}
