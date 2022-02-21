using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MessageDispatcher.SendMessage(Msg.WonGame);
        }
    }
}
