using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void LoadSceneAsync()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
