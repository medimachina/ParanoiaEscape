using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public Transform Model;
    public StringEvent playerStartWalking;
    public UnityEvent playerStopWalking;
    public float speed = 0.2f;
    Rigidbody _rb;

    private bool _startWalkingAudio;
    private bool _stopWalkingAudio;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (TimeMgr.Paused)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = direction * speed;
        if (isMoving(velocity))
        {
            if (_startWalkingAudio)
            {
                playerStartWalking.Invoke("");
                _stopWalkingAudio = true;
            }
            _startWalkingAudio = false;

        }
        else
        {
            if (_stopWalkingAudio)
            {
                playerStopWalking.Invoke();
            }
            _startWalkingAudio = true;
            _stopWalkingAudio = false;
        }
        _rb.velocity = velocity;
        if (direction.magnitude != 0)
        {
            Model.forward = direction;
        }
    }


    bool isMoving(Vector3 vector)
    {
        return vector.x != 0 || vector.z != 0;
    }
}
