using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.2f;
    Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        //_rb.AddForce(new Vector3(horizontal * speed, 0, vertical * speed));
        _rb.velocity = new Vector3(horizontal * speed, 0, vertical * speed);

        //var playerVector = transform.position;
        ////Vector3 newPos = playerVector + new Vector3(0.01f, 0, 0);
        //Vector3 newPos = new Vector3(playerVector.x + (speed * horizontal), 0, playerVector.z + speed * vertical);
        //// playerVector.Set()
        //transform.position = newPos;
        //Debug.Log(newPos);
    }
}
