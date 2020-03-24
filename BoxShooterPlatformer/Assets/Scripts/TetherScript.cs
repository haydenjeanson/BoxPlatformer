using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherScript : MonoBehaviour
{

    public LineRenderer tether;
    private Vector2? tetherPoint = null;
    private GameObject tetherObject = null;

    void FixedUpdate()
    {
        if (tetherPoint != null) {
            tetherPoint = new Vector2(tetherObject.transform.position.x, Globals.Instance.Player.transform.position.y);
            drawLine(gameObject.transform.position, tetherPoint.Value);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && Globals.Instance.isTouchOnShoot(touch)) {
                Tether();
            }
        } else if (Input.GetKeyDown("w")) {
            Tether();
        }
    }

    void Tether() {
        if (tetherPoint == null) {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(Globals.Instance.PlayerDirection, 0), Mathf.Infinity, Globals.Instance.PLAYER_LAYER);
            Vector2 hitPoint = hit.point;
            
            if (Globals.Instance.PlayerDirection > 0) {
                hitPoint.x += hit.collider.gameObject.transform.lossyScale.x / 2;
            } else {
                hitPoint.x -= hit.collider.gameObject.transform.lossyScale.x / 2;
            }

            if (hit.collider.gameObject.tag == "Block") {
                tetherObject = hit.collider.gameObject;

                tetherObject.transform.parent = Globals.Instance.Player.transform;

                tetherPoint = hitPoint;
            }
        } else {
            tetherObject.transform.parent = null;
            tetherObject = null;
            tetherPoint = null;
            drawLine(gameObject.transform.position, gameObject.transform.position);
        }
    }

    void drawLine(Vector2 start, Vector2 end) {
        Vector3[] pos = new Vector3[] {start, end};
        tether.SetPositions(pos);
        tether.startWidth = 0.2f;
    }
}
