using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public input controls;

    public int speed = 950;
    
    public int jumpForce = 35;
    private int maxVerticalSpeed;
    private bool doJump = false;
    public float fallMultiplier = 2.5f;
    private sbyte direction = 1;
    private Rigidbody2D rb;
    private BoxCollider2D objectCollider;  

    void Awake() {
        controls = new input();
        controls.Player.Jump.performed += _ => jump();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
        maxVerticalSpeed = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        // jump();
        // Debug.Log(isGrounded());
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(speed * direction * Time.deltaTime, rb.velocity.y);
        
        if (doJump) { rb.velocity = transform.up * jumpForce; doJump = false; }

        // Stop force from multiplying due to jump spamming
        if (rb.velocity.y > maxVerticalSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.GetContact(0).normal.x != 0)
            direction *= -1;
        
        if (other.gameObject.tag == "Finish")
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    void jump() {
        doJump = false;

        if (isGrounded()) {
            doJump = true;
        }
    }

    private bool isGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(objectCollider.bounds.center, objectCollider.bounds.size, 0f, Vector2.down, objectCollider.bounds.extents.y, LayerMask.GetMask("Platform"));
        return hit.collider != null;
    }
}
