using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockScript : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    private int direction = 1;
    private Rigidbody2D rb;
    private Collider2D objectCollider;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.GetContact(0).normal.x != 0 ) {
            if (other.gameObject.tag != "Player") {
                direction *= -1;
                rb.velocity = new Vector2(rb.velocity.x * direction, rb.velocity.y);
            } else {
                if (other.GetContact(0).normal.x == 1 ) {
                    rb.AddForce(Vector2.right * 12,ForceMode2D.Impulse);
                    direction = 1;
                } else {
                    rb.AddForce(Vector2.right * -12,ForceMode2D.Impulse);
                    direction = -1;
                }
            }
        }
    }
}
