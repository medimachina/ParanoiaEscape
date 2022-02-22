using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGuadGizmos : MonoBehaviour
{
    public float ViewDistance = 5;
    public float ViewAngle = 45;
    public Transform Agent;

    void Start()
    {
        if (Agent == null)
        {
            Agent = gameObject.transform;
        }
    }

    public void OnDrawGizmos()
    {
        if (Agent != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 leftEndPoint = Agent.position + Quaternion.Euler(0, -ViewAngle / 2, 0) * Agent.forward * ViewDistance;
            Vector3 rightEndPoint = Agent.position + Quaternion.Euler(0, ViewAngle / 2, 0) * Agent.forward * ViewDistance;

            Gizmos.DrawLine(Agent.position, leftEndPoint);
            Gizmos.DrawLine(Agent.position, rightEndPoint);
            Gizmos.DrawLine(leftEndPoint, rightEndPoint);
        }
    }

}
