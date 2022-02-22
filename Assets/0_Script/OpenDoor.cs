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
        if (doorAnimationActive == true)
        {
            UpdateDoorPos();
        }
    }

    void UpdateDoorPos()
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

    public void ToggleDoor()
    {
        if (!doorAnimationActive) {
            doorAnimationActive = true;
            _isDoorOpen = !_isDoorOpen;
            _currentProgress = 0;
        }
    }

    public void ForceOpen()
    {
        if (_isDoorOpen == false && doorAnimationActive)
        {
            _currentProgress = 1 - _currentProgress;
        }

        doorAnimationActive = true;
        _isDoorOpen = true;
    }

    public void Close()
    {
        if (_isDoorOpen == true && doorAnimationActive)
        {
            _currentProgress = 1 - _currentProgress;
        }

        doorAnimationActive = true;
        _isDoorOpen = false;
    }
}
