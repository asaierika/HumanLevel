using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingManager : MonoBehaviour
{
    public static FollowingManager instance;
    public GameObject follower;
    // Chasing status of the given follower.
    public bool isFollowing;
    // public VectorValue position;
    public Map castleMap;
    // Delay before the follwing gameobject appears in scene and continues to chase after player.
    public float spawnDelay = 1f;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += delegate { Spawn(); };
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= delegate { Spawn(); };
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
        yield return new WaitForSeconds(spawnDelay);
        GameObject existingFollower = GameObject.FindWithTag("Follower");
        if (existingFollower == null) {
            // GameObject spawnedFollower = Instantiate(follower, position.initialValue, Quaternion.identity);
            GameObject spawnedFollower = Instantiate(follower, castleMap.GetStartingPosition(GameManager.instance.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name), 
                    Quaternion.identity);
        }
    }
}
