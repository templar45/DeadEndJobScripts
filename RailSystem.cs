using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

//Author: Christopher Cruz
public enum PlayMode
{
    Linear,
    Catmull
}
[ExecuteInEditMode]
public class RailSystem : MonoBehaviour
{
    public Transform[] nodes;
    // Start is called before the first frame update
    void Start()
    {
       nodes = GetComponentsInChildren<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 positionOnRail(int seg, float ratio, PlayMode mode)
    {
        switch(mode)
        {
            default:
            case PlayMode.Linear:
                return LinearPosition(seg, ratio);
            case PlayMode.Catmull:
                return CatmullPosition(seg, ratio);
        }
    }

    public Vector3 LinearPosition(int seg, float ratio)
    {
        Vector3 point1 = nodes[seg].position;
        Vector3 point2 = nodes[seg + 1].position;

        return Vector3.Lerp(point1, point2, ratio);
    }

    public Vector3 CatmullPosition(int seg, float ratio)
    {
        Vector3 point1, point2, point3, point4;

        if (seg == 0)
        {
            point1 = nodes[seg].position;
            point2 = point1;
            point3 = nodes[seg + 1].position;
            point4 = nodes[seg + 2].position;
        }
        else if (seg == nodes.Length - 2)
        {
            point1 = nodes[seg - 1].position;
            point2 = nodes[seg].position;
            point3 = nodes[seg + 1].position;
            point4 = point3;
        }
        else
        {
            point1 = nodes[seg - 1].position;
            point2 = nodes[seg].position;
            point3 = nodes[seg + 1].position;
            point4 = nodes[seg + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        float x = .5f * ((2.0f * point2.x)+ (-point1.x + point3.x)* ratio + (2.0f * point1.x - 5.0f * point2.x + 4 * point3.x - point4.x)* t2 + (-point1.x + 3.0f * point2.x - 3.0f * point3.x + point4.x)* t3);
        float y = .5f * ((2.0f * point2.y)+ (-point1.y + point3.y)* ratio + (2.0f * point1.y - 5.0f * point2.y + 4 * point3.y - point4.y)* t2 + (-point1.y + 3.0f * point2.y - 3.0f * point3.y + point4.y)* t3);
        float z = .5f * ((2.0f * point2.z)+ (-point1.z + point3.z)* ratio + (2.0f * point1.z - 5.0f * point2.z + 4 * point3.z - point4.z)* t2 + (-point1.z + 3.0f * point2.z - 3.0f * point3.z + point4.z)* t3);

        return new Vector3(x, y, z);
    }

    public Quaternion rRotation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].rotation;
        Quaternion q2 = nodes[seg+1].rotation;

        return Quaternion.Lerp(q1, q2, ratio);
    }

    private void OnDrawGizmosSelected()
    {
        for( int i = 0; i < nodes.Length - 1; i++)
        {
            //Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3.0f);
			Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
        }
    }
}
