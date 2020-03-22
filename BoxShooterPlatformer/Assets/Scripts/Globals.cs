using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set;}

    private Camera cam;

    private GameObject player;
    public GameObject Player {
        get {
            return player;
        }
    }
    private Rigidbody2D playerRB;

    public GameObject[] GroundObjects { get; set; }

    public GameObject[] jumpButtons = new GameObject[1];
    private float[,] jumpButtonBounds = new float[1, 4];

    public int PLAYER_LAYER = 9;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        for (int i = 0; i < jumpButtons.Length; i++) {
            Transform t = jumpButtons[i].GetComponent<Transform>();

            float top = (t.position + (t.lossyScale / 2)).y;
            float right = (t.position + (t.lossyScale / 2)).x;
            float bot = (t.position - (t.lossyScale / 2)).y;
            float left = (t.position - (t.lossyScale / 2)).x;

            jumpButtonBounds[i, 0] = top;
            jumpButtonBounds[i, 1] = right;
            jumpButtonBounds[i, 2] = bot;
            jumpButtonBounds[i, 3] = left;
        }
    }

    void Update() {

    }

    // Returns direction the player is currently moving. +1 = right, -1 = left
    public int PlayerDirection
    {
        get
        {
            return ((int)playerRB.velocity.x >= 0) ? 1 : -1; ;
        }
    }

    private float getJumpButtonBounds(int buttonIndex, string side) {
        switch (side) {
            case "top":
                return jumpButtonBounds[buttonIndex, 0];
            case "right":
                return jumpButtonBounds[buttonIndex, 1];
            case "bot":
                return jumpButtonBounds[buttonIndex, 2];
            case "left":
                return jumpButtonBounds[buttonIndex, 3];
            default:
                return -999.999f; // Indicates error. -999.999 will never be within screen bounds.
        }
    }
    
    public bool isTouchOnShoot(Touch touch) {
        float touchX = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0)).x;
        float touchY = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0)).y;
        for (int i = 0; i < Globals.Instance.jumpButtons.Length; i++) {
            if ((touchX > getJumpButtonBounds(i, "left") && touchX < getJumpButtonBounds(i, "right")) && (touchY > getJumpButtonBounds(i, "bot") && touchY < getJumpButtonBounds(i, "top"))) {
                return true;
            }
        }

        return false;
    }

}