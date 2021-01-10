using System.Collections;
using UnityEngine;


public class PlayerCheckpointDetection : MonoBehaviour
{
    public GameObject saveGameFX;
    public bool isLoading;

    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        LoadCurrentCheckpoint();
    }

    public void LoadCurrentCheckpoint()
    {
        if (SaveManager.Instance != null)
        {
            if (SaveManager.Instance.isLevelStart == false)
            {
                _transform.rotation = Quaternion.Euler(SaveManager.Instance.currentCheckpointRotation);
                _transform.position = SaveManager.Instance.currentCheckpointPosition;
                isLoading = true;
                StartCoroutine("IsLoadingTimeOut", 1.0f);
            }
        }
    }

    public void SaveCurrentCheckpoint(Vector3 checkpointPosition, Vector3 checkpointRotation)
    {
        SaveManager.Instance.SaveCurrentCheckpoint(checkpointPosition, checkpointRotation);
        if(!isLoading)
        {
            CreateSaveFX(checkpointPosition);
        }
    }


    public void CreateSaveFX(Vector3 checkpointPosition)
    {
        Instantiate(saveGameFX, checkpointPosition, Quaternion.Euler(-75,0,0));
    }


    public IEnumerator IsLoadingTimeOut(float IsLoadingTimer)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f) // In a while loop while counting down
        {
            normalizedTime += Time.deltaTime / IsLoadingTimer;
            yield return null;
        }
        isLoading = false;
    }
}
