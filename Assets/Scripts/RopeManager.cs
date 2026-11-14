using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class RopeManager : MonoBehaviour
{

    [SerializeField] public Transform block;
    [SerializeField] public Transform player;

    
    private GameObject temp;
    Transform tempTransform;
    CircleCollider2D collider;
    
    
    private int segmentCount = 4;
    private Transform[] segments;
    
    public DistanceJoint2D dt;

    

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

       
        

        for (int i = 0; i < segmentCount; i++)
        {
            var curJoint = Instantiate(dt, GetSegPos(i), Quaternion.identity, this.transform);
            //curJoint.distance = 0.05;

            curJoint.GetComponent<Rigidbody2D>().bodyType =
            i == 0  ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;// || 
            segments[i] = curJoint.transform;

            //if(i == segmentCount - 1 && false)

            if(i > 0)
            {
                int prevIndex = i-1;
                curJoint.connectedBody = segments[prevIndex].GetComponent<Rigidbody2D>();
            }
            else
            {
                
            }
            //segments[segmentCount-1] = Instantiate(hinge, block.position, Quaternion.identity, this.transform);
            
            if(i == segmentCount - 1)
            {
                curJoint.AddComponent<CircleCollider2D>();
                collider = curJoint.GetComponent<CircleCollider2D>();
                collider.radius = 1;
                collider.isTrigger = true;
            }
        }
        line.enabled = true;
        line.positionCount = segmentCount;

    }
    
    // Update is called once per frame
    void Update()
    {

        // segments[0].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1, 2);
        // segments[segmentCount-1].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 2);
        segments[0].position = block.position;
        //segments[segmentCount-1].position = player.position;

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
