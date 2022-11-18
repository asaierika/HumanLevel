using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingManager : MonoBehaviour
{
    public static FollowingManager instance;
    public bool isFollowing;
    public GameObject follower;
    public VectorValue position;
    // Delay before the follwing gameobject appears in scene and continues to chase after player.
    public float spawnDelay = 0.5f;

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
            GameObject spawnedFollower = Instantiate(follower, position.initialValue, Quaternion.identity);
            spawnedFollower.SetActive(true);
        }
       
    }
}
