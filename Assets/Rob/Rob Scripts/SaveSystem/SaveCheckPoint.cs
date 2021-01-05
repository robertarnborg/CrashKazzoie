using System;
using UnityEngine;

public class SaveCheckPoint : MonoBehaviour
{
    private Vector3 _checkpointPosition;

    private void Awake()
    {
        _checkpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerCheckpointDetection>() != null)
        {
            var playerCheckPointDetection = other.gameObject.GetComponent<PlayerCheckpointDetection>();
            playerCheckPointDetection.SaveCurrentCheckpoint(_checkpointPosition);
            Debug.Log("This Save Point is Saved!");
        }
    }
}
