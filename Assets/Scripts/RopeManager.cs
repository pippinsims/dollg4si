using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]

public class RopeManager : MonoBehaviour
{

    [SerializeField] public Transform block;
    public Transform player;

    
    private GameObject temp;
    Transform tempTransform;
    CircleCollider2D circCollider;
    
    FixedJoint2D fixedJ; 
    private int segmentCount = 10;
    private Transform[] segments;
    
    public SpringJoint2D spring;

    public PlayerControl playerControl;
    public bool grabbed;
    LineRenderer line; 

    Vector2 GetSegPos(int segmentIndex)
    {
        Vector2 posA = block.position;
        Vector2 posB = tempTransform.position;
        
        float frac = 1f/ (float)segmentCount;
        return Vector2.Lerp(posA, posB, frac * segmentIndex);
    }


    void Start()
    {
        line = GetComponent<LineRenderer>();  
        segments = new Transform[segmentCount];
        temp = new GameObject("temp");
        tempTransform = temp.transform;
        tempTransform.position = new Vector3(5, 0, 0);
        tempTransform.rotation = Quaternion.identity;

        player= GameObject.Find("Player").GetComponent<Transform>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    
        fixedJ = this.GetComponentInChildren<FixedJoint2D>();
        
        

        for (int i = 0; i < segmentCount; i++)
        {
            var curJoint = Instantiate(spring, GetSegPos(i), Quaternion.identity, this.transform);
            //curJoint.distance = 0.05;

            //curJoint.GetComponent<Rigidbody2D>().bodyType =  i == 0  ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic; 
            segments[i] = curJoint.transform;

            //if(i == segmentCount - 1 && false)

            if(i > 0)
            {
                int prevIndex = i-1;
                curJoint.connectedBody = segments[prevIndex].GetComponent<Rigidbody2D>();
            }
            else
            {
                fixedJ.connectedBody = curJoint.attachedRigidbody;
                curJoint.enabled=false;
            }
            //segments[segmentCount-1] = Instantiate(hinge, block.position, Quaternion.identity, this.transform);
            
            if(i == segmentCount - 1)
            {
                circCollider = curJoint.AddComponent<CircleCollider2D>();
                circCollider.radius = 1;
                circCollider.isTrigger = true;
                
            }
        }
        line.enabled = true;
        line.positionCount = segmentCount;
        Destroy(spring.gameObject);

    }
    
    // Update is called once per frame
    void Update()
    {
        
        // segments[0].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1, 2);
        // segments[segmentCount-1].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 2);
        segments[0].position = block.position;
        grabbed = playerControl.grabbed;
        if (grabbed)
        {
            segments[segmentCount-1].position = player.position;
        }
        else
        {
            
        }
        

        for(int i = 0; i <segments.Length; i++)
        {
            line.SetPosition(i, segments[i].position);
        }

        
        
    }

    private void ODrawGizmos()
    {
        if(tempTransform == null ||  block == null) return;
        Gizmos.color = UnityEngine.Color.green;

        for(int i = 0; i < segmentCount; i++)
        {
            Vector2 pos = GetSegPos(i);
            Gizmos.DrawSphere(pos, 0.1f);
        }

    }


    
}
