using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.P))
        {
            TimeMgr.Toggle();
        }
    }
#endif
}
