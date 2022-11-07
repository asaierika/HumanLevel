using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingChoice : MonoBehaviour
{
    public VectorValue playerPosition;

    public void Choice1()
    {
        playerPosition.initialValue = new Vector2(1.2f, -1f);
        FollowingManager.instance.isFollowing = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Castle_1stHall");
    }

    public void Choice2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Castle_1stHall_beforeFollowing");
    }
}
