using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement cameraInstance;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float cameraSpeed;
    private Vector3 targetPosition; 
    private Vector3 currentVelocity = Vector3.zero;
    public bool isFrozen = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraInstance = this;
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z-1f); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isFrozen){
            targetPosition  = new Vector3(playerPosition.position.x, playerPosition.position.y+3, transform.position.z); 
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, cameraSpeed);
        }
    }
}
