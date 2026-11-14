using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class RopeManager : MonoBehaviour
{

    [SerializeField] public Transform block;
    [SerializeField] public Transform player;
    
    
    
    public int segmentCount = 4;
    public Transform[] segments;
    
    public DistanceJoint2D dt;

    LineRenderer line; 

    Vector2 GetSegPos(int segmentIndex)
    {
        Vector2 posA = block.position;
        Vector2 posB = player.position;
        
        float frac = 1f/ (float)segmentCount;
        return Vector2.Lerp(posA, posB, frac * segmentIndex);
    }


    void Start()
    {
        line = GetComponent<LineRenderer>();  
        segments = new Transform[segmentCount];

        for(int i = 0; i < segmentCount; i++)
        {
            var curJoint = Instantiate(dt, GetSegPos(i), Quaternion.identity, this.transform);
            //curJoint.distance = 0.05;

            curJoint.GetComponent<Rigidbody2D>().bodyType =
            i == 0 || i == segmentCount - 1 ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
            segments[i] = curJoint.transform;

            if(i > 0)
            {
                int prevIndex = i-1;
                curJoint.connectedBody = segments[prevIndex].GetComponent<Rigidbody2D>();
            }
            else
            {
                
            }
            //segments[segmentCount-1] = Instantiate(hinge, block.position, Quaternion.identity, this.transform);
            

        }
        line.enabled = true;
        line.positionCount = segmentCount;
    }
    
    // Update is called once per frame
    void Update()
    {

        // segments[0].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1, 2);
        // segments[segmentCount-1].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 2);
        segments[0].position = player.position;
        segments[segmentCount-1].position = block.position;

        for(int i = 0; i <segments.Length; i++)
        {
            line.SetPosition(i, segments[i].position);
        }

        
    }

    private void ODrawGizmos()
    {
        if(player == null ||  block == null) return;
        Gizmos.color = UnityEngine.Color.green;

        for(int i = 0; i < segmentCount; i++)
        {
            Vector2 pos = GetSegPos(i);
            Gizmos.DrawSphere(pos, 0.1f);
        }

    }
}
