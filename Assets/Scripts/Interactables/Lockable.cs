using UnityEngine;

public class Lockable : MonoBehaviour
{
    private bool isLocked = false;
    public string key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == key) {
            this.isLocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == key) {
            this.isLocked = false;
        }
    }

    public bool IsLocked => this.isLocked;
}
