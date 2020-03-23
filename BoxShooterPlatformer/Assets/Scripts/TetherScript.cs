using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherScript : MonoBehaviour
{

    public LineRenderer tether;
    private Vector2? tetherPoint = null;

    void FixedUpdate()
    {
        if (tetherPoint != null) {
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
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(Globals.Instance.PlayerDirection, 0), Mathf.Infinity, Globals.Instance.PLAYER_LAYER);
        Debug.DrawRay(gameObject.transform.position, new Vector2(Globals.Instance.PlayerDirection, 0), Color.magenta, 0.1f);
        Debug.Log(hit.point);

        tetherPoint = hit.point;
    }

    void drawLine(Vector2 start, Vector2 end) {
        Vector3[] pos = new Vector3[] {start, end};
        tether.SetPositions(pos);
    }
}
