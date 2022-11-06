using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_corridor : Collidable
{
    public string followingScene;
    public string notFollowingScene;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    protected override void OnCollide(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            playerStorage.initialValue = playerPosition;

            if(FollowingManager.instance == null || !FollowingManager.instance.isFollowing) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(notFollowingScene);
            } else {
                FollowingManager.instance.passedPortal = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(followingScene);
            }
        }
    }
}
