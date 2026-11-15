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
    public Camera mainCam;
<<<<<<< HEAD:Assets/_Scripts/PlayerControl.cs
=======

    public bool climb;
>>>>>>> f68fdc1ec05d82c467a40d9da0e20457f9c21eee:Assets/Scripts/PlayerControl.cs
    bool jump;
    bool stunned;

    GameObject head;
    SpriteRenderer headrend;
    SpriteRenderer rend;

    public Sprite ur,um,mr,mm,dr,dm;
    private Animator animator;

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
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jump && !stunned)
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
        if(rb.linearVelocity.x != 0) rend.flipX = rb.linearVelocity.x < 0;
        animator.SetBool("isFalling", rb.linearVelocity.y < -1.5f);
        animator.SetBool("isClimbing", climb);
        animator.speed = climb && (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed) || !climb ? 1f : 0f;
        stunned = animator.GetCurrentAnimatorStateInfo(0).IsName("GetUp");
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
