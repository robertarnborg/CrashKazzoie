using System;
using UnityEngine;

public class SaveCheckPoint : MonoBehaviour
{
    [SerializeField]
    public string thisCheckPoint;

    [NonSerialized]
    private Vector3 _checkpointPosition;


    private void Awake()
    {
        _checkpointPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // TO DO Add Tag or Id for Player Detection
        SaveCurrentCheckpoint();
    }

    private void SaveCurrentCheckpoint()
    {
        SaveManager.Instance.SaveCurrentCheckpoint(_checkpointPosition);
        Debug.Log("This Save Point = " + thisCheckPoint + " is Saved!");
    }
}
