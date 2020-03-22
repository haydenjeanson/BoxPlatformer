using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public byte speed = 15;
    
    public int jumpForce = 35;
    private int maxVerticalSpeed;
    private bool canJump = false;
    public float fallMultiplier = 2.5f;
    private sbyte direction = 1;
    private Rigidbody2D rb;
    private Collider2D objectCollider;
    // private GameObject[] groundObjects;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
        maxVerticalSpeed = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        jump();
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        
        // Stop force from multiplying due to jump spamming
        if (rb.velocity.y > maxVerticalSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.GetContact(0).normal.x != 0)
            direction *= -1;

        if (other.GetContact(0).normal.y > 0)
            canJump = true; 
        
        if (other.gameObject.tag == "Finish")
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void OnCollisionStay2D (Collision2D other) {
        Debug.Log(other.GetContact(0).normal.y);
        if (other.GetContact(0).normal.y > 0)
            canJump = true; 
    }

    void OnCollisionExit2D(Collision2D other) {
        canJump = false;
    }

    void jump() {
        bool doJump = false;
        
        if (canJump) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && !Globals.Instance.isTouchOnShoot(touch)) {
                    doJump = true;
                }
            } else if (Input.GetKeyDown("space")) {
                doJump = true;
            }
        }

        if (doJump) { rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); }
    }
}
