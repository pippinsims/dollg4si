using System;
using System.Runtime.InteropServices;
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
    Camera mainCam;
    SpriteRenderer rend;
    FixedJoint2D fixedJ;

    Rigidbody2D nearestRope;

    public bool climb;
    bool jump;
    bool stunned;
    public bool unstun;

    GameObject head;
    SpriteRenderer headrend;

    public Sprite ur,um,mr,mm,dr,dm;
    private Animator animator;

    private bool canGrab;
    public bool grabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var x = transform.GetChild(0).gameObject;
        rend = x.GetComponent<SpriteRenderer>();
        head = x.transform.GetChild(0).gameObject;
        headrend = head.GetComponent<SpriteRenderer>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        fixedJ = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jump && !stunned)
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jump = false;
        }

        var curMouse = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float thresh = 1f;
        bool u = curMouse.y > transform.position.y + thresh;
        bool mv = curMouse.y >= transform.position.y - thresh && curMouse.y <= transform.position.y + thresh;

        if(curMouse.x >= transform.position.x - thresh && curMouse.x <= transform.position.x + thresh)
        {
            if(u) headrend.sprite = um;
            else if(mv) headrend.sprite = mm;
            else headrend.sprite = dm;
        }
        else
        {
            headrend.flipX = curMouse.x <= transform.position.x + thresh;
            if(u) headrend.sprite = ur;
            else if(mv) headrend.sprite = mr;
            else headrend.sprite = dr;
        }

        animator.SetBool("isWalking", MathF.Abs(rb.linearVelocity.x) + 1f > speed);
        if(MathF.Abs(rb.linearVelocity.x) > 1f ) rend.flipX = rb.linearVelocity.x < 0;
        animator.SetBool("isFalling", rb.linearVelocity.y < -1.5f);
        animator.SetBool("isClimbing", climb);
        animator.SetBool("unstun", unstun);
        animator.speed = climb && (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed) || !climb ? 1f : 0f;
        stunned = animator.GetCurrentAnimatorStateInfo(0).IsName("GetUp");
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
        if (Keyboard.current.dKey.isPressed && !stunned)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else if (Keyboard.current.aKey.isPressed && !stunned)
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
            unstun = c.collider.CompareTag("Boulder");
        }

        if (c.collider.CompareTag("Door"))
            SceneManager.LoadScene("Backyard");
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
