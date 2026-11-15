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
    FixedJoint2D fixedJ;

    Rigidbody2D nearestRope;

    public bool climb;
    bool jump;

    private bool canGrab;
    public bool grabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJ = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            if(grabbed)
            {
                grabbed = !grabbed;
                fixedJ.enabled = false; 
                fixedJ.connectedBody=null;     
            }
            else if(!grabbed && canGrab)
            {
                grabbed = !grabbed;
                fixedJ.enabled=true;
                fixedJ.connectedBody = nearestRope;
            }

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
        if (c.collider.CompareTag("Ground"))
        {
            jump = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("BoxRope"))
        {
            canGrab= true;
            nearestRope = c.attachedRigidbody;
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

        if (c.CompareTag("BoxRope"))
        {
            canGrab=false;
        }
    }

    public static explicit operator PlayerControl(GameObject v)
    {
        throw new NotImplementedException();
    }
}
