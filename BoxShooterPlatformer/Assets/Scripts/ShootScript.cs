using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Keyboard input
        if (Input.GetKeyDown("w")) {
            shoot();
        }

        // Touch Input
        Touch touch;
        if ((Input.touchCount > 0 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began)) {
            if (Globals.Instance.isTouchOnShoot(touch)) {
                shoot();
            }
        }
    }

    public void shoot() {
        //The Bullet instantiation happens here.
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet,Bullet_Emitter.transform.position,Bullet_Emitter.transform.rotation) as GameObject;

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
        Temporary_RigidBody.AddForce(transform.right * Bullet_Forward_Force * Globals.Instance.PlayerDirection);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds
        Destroy(Temporary_Bullet_Handler, 10.0f);
    }
}