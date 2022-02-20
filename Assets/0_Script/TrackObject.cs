using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
