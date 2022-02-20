using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform doorTransform;
    public float AnimationSpeed = 0.002f;
    public float OpenAmount = 2;
    public AnimationCurve speedCurve;
    private bool _isDoorOpen = false;
    private Vector3 closedState;
    private Vector3 openState;
    private bool doorAnimationActive = false;
    private float _currentProgress = 0;

    void Start()
    {
        closedState = doorTransform.localPosition;
        openState = closedState + new Vector3(-OpenAmount, 0, 0);
    }

    void Update()
    {
        if (!doorAnimationActive && Input.GetKeyDown(KeyCode.E))
        {
            doorAnimationActive = true;
            _isDoorOpen = !_isDoorOpen;
            _currentProgress = 0;
        }
        if (doorAnimationActive == true)
        {
            SetDoorState();
        }
    }

    void SetDoorState()
    {
        _currentProgress += AnimationSpeed;
        float doorCurrentX = doorTransform.localPosition.x;
        if (_isDoorOpen && (doorCurrentX > openState.x))
        {
            doorTransform.localPosition = Vector3.Lerp(closedState, openState, speedCurve.Evaluate(_currentProgress));
        } else if (_isDoorOpen && (doorCurrentX <= openState.x))
        {
            doorAnimationActive = false;
        }
        else if (!_isDoorOpen && (doorCurrentX < closedState.x))
        {
            doorTransform.localPosition = Vector3.Lerp(openState, closedState, speedCurve.Evaluate(_currentProgress));
        }
        else if (!_isDoorOpen && (doorCurrentX >= closedState.x))
        {
            doorAnimationActive = false;
        }
    }
}
