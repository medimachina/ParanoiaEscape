using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class NavmeshAgentTest : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent _navMeshAgent;
    [SerializeField]
    Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        if (_navMeshAgent == null)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        SetDestination();
    }

    [Button("Set destination")]
    void SetDestination()
    {
        if (_target != null)
        {
            _navMeshAgent.destination = _target.position;
        }
    }
}

