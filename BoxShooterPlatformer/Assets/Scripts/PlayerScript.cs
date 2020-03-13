using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public byte speed = 15;
    public int maxVerticalSpeed = 30;
    public int jumpForce = 2500;
    public float fallMultiplier = 2.5f;
    private sbyte direction = 1;
    private Rigidbody2D rb;
    private Collider2D objectCollider;
    private GameObject[] groundObjects;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();

        groundObjects = GameObject.FindGameObjectsWithTag("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        
        // Stop force from multiplying due to jump spamming
        if (rb.velocity.y > maxVerticalSpeed) {
            rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);
        }

        jump();

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Side") {
            direction *= -1;
            // rb.velocity = new Vector2(0, rb.velocity.y);
        } else if (other.gameObject.tag == "Finish") {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    void jump() {
        bool doJump = false;
        if (isTouchingGround()) {



            // doJump = true; // REMOVE AFTER DONE TESTING



            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && !Globals.Instance.isTouchOnShoot(touch)) {
                    doJump = true;
                }
            } else if (Input.GetKeyDown("space")) {
                doJump = true;
            }
        }

        if (doJump) { rb.AddForce(transform.up * jumpForce); }
    }

    private bool isTouchingGround() {
        foreach (GameObject ground in groundObjects) {
            if (objectCollider.IsTouching(ground.GetComponent<BoxCollider2D>())) {
                return true;
            }
        }

        return false;
    }
}
