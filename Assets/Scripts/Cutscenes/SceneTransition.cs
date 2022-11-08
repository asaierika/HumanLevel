using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneName;

    public void SceneTransit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
