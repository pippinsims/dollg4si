using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class Rolling : MonoBehaviour
{
    private float speed;
    private PlayerControl playerControl;
    Rigidbody2D rb;
    private bool isDead = false;
    SpriteRenderer rend;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    private void OnCollisionStay2D(Collision2D c)
    {
        if (c.collider.CompareTag("Player")){
            if(rb.position.y > player.position.y && !isDead)
            {
                StartCoroutine(playerControl.KillPlayer());
                isDead = true;
            }
        }
    }

}
