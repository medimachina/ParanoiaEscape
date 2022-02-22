using System;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerEnterCheck : MonoBehaviour
{
    public OpenDoor openDoor;
    private List<Collider> _enemiesInsideTrigger;
    private bool _playerIsNear = false;
    private bool _enemyWasNerLastFrame = false;
    private bool EnemyIsNear
    {
        get
        {
            return _enemiesInsideTrigger.Count > 0;
        }
    }

    private void Awake()
    {
        _enemiesInsideTrigger = new List<Collider>();
    }

    private void Update()
    {
        if (_playerIsNear && !EnemyIsNear && Input.GetKeyDown(KeyCode.E))
        {
            openDoor.ToggleDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIsNear = true;
        }
        else if (other.tag == "Enemy")
        {
            if (!_enemiesInsideTrigger.Contains(other))
            {
                _enemiesInsideTrigger.Add(other);
            }

            Debug.Log($"Checking for enemy on enter. Count: {_enemiesInsideTrigger.Count}, EnemyIsNear: {EnemyIsNear}");
            CheckIfToggleForEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIsNear = false;
        }
        else if (other.tag == "Enemy")
        {
            if (_enemiesInsideTrigger.Contains(other))
            {
                _enemiesInsideTrigger.Remove(other);
            }

            Debug.Log($"Checking for enemy on exit. Count: {_enemiesInsideTrigger.Count}, EnemyIsNear: {EnemyIsNear}");
            CheckIfToggleForEnemy();
        }
    }

    private void CheckIfToggleForEnemy()
    {
        if (EnemyIsNear != _enemyWasNerLastFrame)
        {
            _enemyWasNerLastFrame = EnemyIsNear;
            ToggleForEnemy();
        }
    }

    private void ToggleForEnemy()
    {
        if (EnemyIsNear)
        {
            Debug.Log("Force open");
            openDoor.ForceOpen();
        }
        else
        {
            Debug.Log("Close");
            openDoor.Close();
        }
    }
}
