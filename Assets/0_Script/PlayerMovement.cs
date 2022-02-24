using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        var newVector = new Vector3(horizontal, 0f, vertical).normalized *  speed;
        if (isMoving(newVector))
        {
            if (_startWalkingAudio)
            {
                playerStartWalking.Invoke("");
                _stopWalkingAudio = true;
            }
            _startWalkingAudio = false;

        } else
        {
            if (_stopWalkingAudio)
            {
                playerStopWalking.Invoke();
            }
            _startWalkingAudio = true;
            _stopWalkingAudio = false;
        }
        _rb.velocity = newVector;
    }

    bool isMoving(Vector3 vector)
    {
        return vector.x != 0 || vector.z != 0;
    }
}
