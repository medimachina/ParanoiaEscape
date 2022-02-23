using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    Transform _center;

    void Awake()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        _center = renderer.transform;
        renderer.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (_center != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_center.position, 0.1f);
        }
    }
}
