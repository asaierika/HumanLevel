using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static bool hasDefaultSpawnPosition;
    public static Vector2 startLocationIndependentSpawnPoint;
    public Map map;
    public Transform player;

    public static void AssignSpawnPoint(Vector2 position) {
        hasDefaultSpawnPosition = true;
        startLocationIndependentSpawnPoint = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (hasDefaultSpawnPosition) {
            Debug.Log("Spawning at default location.");
            player.transform.position = startLocationIndependentSpawnPoint;
            hasDefaultSpawnPosition = false;
        } else if (PlayerMovement.inContinuousLocations) {
            Debug.Log("Spawning to maintain continous movement");
            try {
                player.transform.position = map.GetStartingPosition(GameManager.instance.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            } catch(UnityException error) {
                Debug.Log(error.Message);
            }
        }
    }
}
