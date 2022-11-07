using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingManager : MonoBehaviour
{
    public static FollowingManager instance;
    public bool isFollowing;
    public GameObject follower;
    public VectorValue position;
    public VectorValue playerPosition;

    private void Awake()
    {
        if (FollowingManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
       
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    void Start()
    {
        SceneManager.sceneLoaded += delegate { Spawn(); };
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void Spawn()
    {
        if (isFollowing)
        {
            StartCoroutine(SpawnFollower());
        }
    }

    IEnumerator SpawnFollower()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject existingFollower = GameObject.FindWithTag("Follower");
        if (existingFollower == null) {
            GameObject spawnedFollower = Instantiate(follower);
            spawnedFollower.transform.position = position.initialValue;
            spawnedFollower.SetActive(true);
        }
       
    }

    public void Choice1()
    {
        playerPosition.initialValue = new Vector2(1.2f, -1f);
        isFollowing = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Castle_1stHall");
    }

    public void Choice2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Castle_1stHall_beforeFollowing");
    }
}
