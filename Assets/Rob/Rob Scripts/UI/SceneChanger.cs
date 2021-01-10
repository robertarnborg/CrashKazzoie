using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneToChangeTo;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
