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
            if (SaveManager.Instance.currentCheckpointPosition != null)
            {
                _transform.position = SaveManager.Instance.currentCheckpointPosition;
                isLoading = true;
                StartCoroutine("IsLoadingTimeOut", 1.0f);
            }
        }
    }

    public void SaveCurrentCheckpoint(Vector3 checkpointPosition)
    {
        SaveManager.Instance.SaveCurrentCheckpoint(checkpointPosition);
        if(!isLoading)
        {
            CreateSaveFX();
        }
    }


    public void CreateSaveFX()
    {
        Instantiate(saveGameFX, _transform.position, _transform.rotation);
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
