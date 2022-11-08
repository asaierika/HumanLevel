using UnityEngine;

public class Lockable : MonoBehaviour
{
    public bool isLocked = false;
    public string otherObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == otherObject) {
            this.isLocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == otherObject) {
            this.isLocked = false;
        }
    }

    public bool IsLocked => this.isLocked;
}
