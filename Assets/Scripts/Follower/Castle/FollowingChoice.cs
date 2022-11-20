using UnityEngine;

public class FollowingChoice : MonoBehaviour
{
    public Vector2 hallPosition;

    public void Choice1()
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        FollowingManager.instance.StartFollowing();
        SceneTransition.SceneTransit("Hall");
    }

    public void Choice2()
    {
        PlayerSpawner.AssignSpawnPoint(hallPosition);
        // Remove the singleton
        Destroy(FollowingManager.instance);
        SceneTransition.SceneTransit("Hall_Before_Chase");
    }
}
