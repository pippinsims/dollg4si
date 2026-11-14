using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CatSightController : MonoBehaviour
{   
    [SerializeField] private CoverController coverController;
    private bool isPlayerInSight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInSight == true && coverController.IsPlayerBehindCover)
        {
            print("Player behind cover.");
        }
        else if (isPlayerInSight == true && coverController.IsPlayerBehindCover == false)
        {
            print("Player attacked");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isPlayerInSight = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isPlayerInSight = false;
    }

}
