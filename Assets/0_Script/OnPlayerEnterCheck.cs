using UnityEngine;

public class OnPlayerEnterCheck : MonoBehaviour
{
    public OpenDoor openDoor;
    private bool _playerIsNear = false;

    private void Update()
    {
        if (_playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            openDoor.ToggleDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerIsNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerIsNear = false;
    }
}
