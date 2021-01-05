using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance { get { return _instance; } }

    public GameObject player;

    #region Save Data

    public bool isPersistentDontDestroyOnLoad = true;

    [SerializeField]
    private Vector3 _currentCheckpointPosition;

    private Scene _currentLevel;
    #endregion
    private void Awake()
    {
        if (isPersistentDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        player = GameObject.FindWithTag("Player"); // Whatever player reference we need

    }

    private void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Start()
    {
        LoadCurrentCheckpoint();
    }

    public void SaveCurrentCheckpoint(Vector3 checkpointPosition)
    {
        _currentCheckpointPosition = checkpointPosition;
    }

    public void LoadCurrentCheckpoint()
    {
        if(player != null)
        {
            player.transform.position = _currentCheckpointPosition;
        }
    }

}
