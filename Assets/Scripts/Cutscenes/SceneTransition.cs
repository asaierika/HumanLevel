using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneName;

    public void SceneTransit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
