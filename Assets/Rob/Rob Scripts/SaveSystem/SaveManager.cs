using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance { get { return _instance; } }

    public bool isPersistentDontDestroyOnLoad = true;
    public bool isLevelStart = true;

    #region Save Data


    public Vector3 currentCheckpointPosition;

    public Scene currentLevel;
    #endregion
    public void Awake()
    {
        if (isPersistentDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else // When First Instantiating
        {
            _instance = this;
        }
    }

    public void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadCurrentCheckPoint();
        }
    }

    public void SaveCurrentCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpointPosition = checkpointPosition;
    }


    public void LoadCurrentCheckPoint()
    {
        isLevelStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ResetCheckPoint()
    {
        currentCheckpointPosition = new Vector3();
    }
}
