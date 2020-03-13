using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D other) {
        Destroy(gameObject);
    }
}
