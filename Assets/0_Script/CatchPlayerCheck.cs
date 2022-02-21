using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class CatchPlayerCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game over!");
            MessageDispatcher.SendMessage(Msg.LostGame);
        }
    }
}
