using System;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField]private Transform playerPosition;
    private Vector3 targetPosition; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z-1f); 
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition  = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z); 
        Vector3 velocity = (targetPosition - transform.position)*moveSpeed;
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime);
    }
}
