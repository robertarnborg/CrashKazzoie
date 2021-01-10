using UnityEngine;

public class ActorPlayer : MonoBehaviour
{
    #region MakeIntoStatic
    public static ActorPlayer Instance { get; private set; }

    public bool isPersistentDontDestroyOnLoad = false;

    private void MakeStaticInstance()
    {
        if (isPersistentDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [HideInInspector]public Transform _transform;

    private void Awake()
    {
        _transform = transform;
        MakeStaticInstance();
    }
}
