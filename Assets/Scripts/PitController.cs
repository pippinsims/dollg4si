using Unity.VectorGraphics;
using UnityEngine;

public class PitController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private PlayerControl playerControl;
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraMovement.cameraInstance.isFrozen = true;
            StartCoroutine(playerControl.KillPlayer());
        }
    }
}
