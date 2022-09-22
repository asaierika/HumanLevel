using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingManager : MonoBehaviour
{
    public static FollowingManager instance;
    public static bool isFollowing;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void SwitchScene(Vector2 position)
    {
        StartCoroutine(SpawnFollower(position));
    }

    IEnumerator SpawnFollower(Vector2 position)
    {
        GameObject follower = GameObject.FindWithTag("Follower");
        follower.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        follower.transform.position = position;
        follower.SetActive(true);
    }
}
