using System;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TestTools;

public class CatSightController : MonoBehaviour
{   
    private CoverController coverController;
    private PlayerControl playerControl;
    private bool isPlayerInSight;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator catAnimator;
    void Start()
    {
        isPlayerInSight = false;
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        catAnimator = GameObject.Find("Cat").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInSight == true && playerControl.isBehindCover)
        {
            print("Player behind cover.");
        }
        else if (isPlayerInSight == true && playerControl.isBehindCover == false)
        {
            print("Player attacked"); 
            if(!isDead) {
                catAnimator.SetBool("IsAttacking", true);
                StartCoroutine(playerControl.KillPlayer());
                isDead = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player"))
            isPlayerInSight = true;
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player"))
            isPlayerInSight = false;
    }

}
