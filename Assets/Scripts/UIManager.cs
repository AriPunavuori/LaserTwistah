using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour{
    Camera cam;
    public TextMeshProUGUI UIText;
    public TextMeshProUGUI UIText2;
    public GameObject circle;
    public float touchRadius = .5f;
    float speed = .25f;
    Dictionary<int, Rigidbody2D> drags = new Dictionary<int, Rigidbody2D>();
    Dictionary<int, Vector2[]> touchVel = new Dictionary<int, Vector2[]>();

    private void Start() {
        cam = Camera.main;
        Application.targetFrameRate = 60;
    }

    void Update() {
        // Loop through touches
        for(int i = 0; i < Input.touchCount; i++) {
            var t = Input.touches[i];
            var s = cam.ScreenToWorldPoint(t.position);
            s.z = 0;

            // Check if object is draggable, add to dictionary and make array for velEst
            if (t.phase == TouchPhase.Began) {
                var col = Physics2D.OverlapCircleAll(s, touchRadius);
                foreach(var c in col) {
                    if(c.GetComponent<Interaction>().IsDraggable()) {
                        drags.Add(t.fingerId, c.GetComponent<Rigidbody2D>());
                        touchVel.Add(t.fingerId, new Vector2[3]);
                    }
                }
            }

            // Give rb velocity and remove from dictionarys
            else if(t.phase == TouchPhase.Ended) {
                var ar = touchVel[t.fingerId];
                var velEst = (ar[0] + ar[1] + ar[2]) / 3;
                drags[t.fingerId].velocity = velEst * speed;
                touchVel.Remove(t.fingerId);
                drags.Remove(t.fingerId);
            }

            // Move object to finger position
            else if (drags.ContainsKey(t.fingerId)) {
                drags[t.fingerId].velocity = Vector2.zero;
                drags[t.fingerId].MovePosition(s);
                var ar = touchVel[t.fingerId];
                ar[2] = ar[1];
                ar[1] = ar[0];
                ar[0] = t.deltaPosition;
            }
        }
    }
}
