using System;
using UnityEngine;

public class SaveCheckPoint : MonoBehaviour
{
    [SerializeField]private Transform checkpointPosition;
    [SerializeField]private Vector3 checkpointRotation;
    private Vector3 _checkpointPosition;
    private bool isTriggered;

    private StandardUnityEvent saveLoadMessage;

    private void Awake()
    {
        _checkpointPosition = checkpointPosition.position;
        saveLoadMessage = GetComponent<StandardUnityEvent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerCheckpointDetection>() != null)
        {
            var playerCheckPointDetection = other.gameObject.GetComponent<PlayerCheckpointDetection>();
            if (!isTriggered)
            {
                if(SaveManager.Instance.currentCheckpointPosition != _checkpointPosition)
                {
                    playerCheckPointDetection.SaveCurrentCheckpoint(_checkpointPosition, checkpointRotation);
                    isTriggered = true;
                    saveLoadMessage.ActivateTrigger();
                }
            }
        }
    }
}
