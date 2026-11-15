using UnityEngine;

public class CoverController : MonoBehaviour
{   
    private PlayerControl playerControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
            playerControl.isBehindCover = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerControl.isBehindCover = false;
    }
}

