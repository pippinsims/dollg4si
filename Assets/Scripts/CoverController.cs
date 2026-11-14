using UnityEngine;

public class CoverController : MonoBehaviour
{   
    private bool isPlayerBehindCover;
    public bool IsPlayerBehindCover => isPlayerBehindCover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isPlayerBehindCover = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isPlayerBehindCover = false;
    }
}

