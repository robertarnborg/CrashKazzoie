using System;
using UnityEngine;

public class SaveCheckPoint : MonoBehaviour
{
    [SerializeField]private Vector3 checkpointRotation;
    private Vector3 _checkpointPosition;
    private bool isTriggered;

    private void Awake()
    {
        _checkpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerCheckpointDetection>() != null)
        {
            var playerCheckPointDetection = other.gameObject.GetComponent<PlayerCheckpointDetection>();
            if (!isTriggered)
            {
                playerCheckPointDetection.SaveCurrentCheckpoint(_checkpointPosition, checkpointRotation);
                isTriggered = true;
            }
        }
    }
}
