using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Custom")]
public class CanSeePlayer : Conditional
{
    [Tooltip("The agent looking for the player")]
    public Transform Agent;
    [Tooltip("How far can the the agent see")]
    public float ViewDistance = 5;
    [Tooltip("View angle of agent")]
    public float ViewAngle = 45;
    [Tooltip("Which layers should block the raycast (e.g. walls and the player herself)")]
    public LayerMask RaycastBlockLayers;
    [Tooltip("The object to look for")]
    public SharedTransform Player;

    private bool _withinFieldOfVision;
    private Vector3 _rayOrigin;
    private Vector3 _rayDirection;
    private float _rayDistanceToHit;
    private bool _rayPlayerHit;

    public override void OnAwake()
    {
        _withinFieldOfVision = false;
        _rayOrigin = new Vector3();
        _rayDirection = new Vector3();
        _rayDistanceToHit = ViewDistance;
        _rayPlayerHit = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (TrySeePlayer())
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    public override void OnReset()
    {

    }

    private GameObject FindPlayerObject()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var obj in playerObjects)
        {
            if (obj.GetComponent<PlayerMovement>() != null)
            {
                return obj;
            }
        }

        Debug.LogWarning("CanSeePlayer task could not find an object with the tag Player and script PlayerMovement!");

        return null;
    }


    private bool TrySeePlayer()
    {
        _withinFieldOfVision = false;

        if (Vector3.Distance(Player.Value.position, Agent.position) > ViewDistance)
        {
            return false;
        }

        if (Vector3.Angle(Agent.forward, (Player.Value.position - Agent.position)) > ViewAngle)
        {
            return false;
        }

        _withinFieldOfVision = true;

        if (!CheckFreeLineOfSight())
        {
            return false;
        }


        return true;
    }

    private bool CheckFreeLineOfSight()
    {
        RaycastHit hit;
        _rayOrigin = Agent.position + Vector3.up;
        _rayDirection = (Player.Value.position - Agent.position).normalized;
        _rayDistanceToHit = ViewDistance;
        _rayPlayerHit = false;

        if (Physics.Raycast(new Ray(_rayOrigin, _rayDirection), out hit, maxDistance:ViewDistance, layerMask: RaycastBlockLayers))
        {
            _rayDistanceToHit = Vector3.Distance(_rayOrigin, hit.point);

            if (hit.collider.tag == "Player")
            {
                _rayPlayerHit = true;
                return true;
            }
        }

        return false;
    }

    public override void OnDrawGizmos()
    {
        if (_withinFieldOfVision)
        {
            Gizmos.color = _rayPlayerHit ? Color.green : Color.red;
            Gizmos.DrawLine(_rayOrigin, _rayOrigin + _rayDirection * _rayDistanceToHit);
        }
    }

}
