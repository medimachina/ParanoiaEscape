using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with door detected with object: " + collision.gameObject.tag); 
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Victory");
            MessageDispatcher.SendMessage(Msg.WonGame);
        }
    }
}
