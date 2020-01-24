using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour{
    Camera cam;
    public TextMeshProUGUI text;
    public GameObject circle;
    public float touchRadius = 0.5f;
    Dictionary<int, Rigidbody2D> drags = new Dictionary<int, Rigidbody2D>();

    private void Start() {
        cam = Camera.main;
    }

    void Update() {
        for(int i = 0; i < Input.touchCount; i++) {
            var t = Input.touches[i];
            var s = cam.ScreenToWorldPoint(t.position);
            s.z = 0;
            if (t.phase == TouchPhase.Began) {
                var col = Physics2D.OverlapCircleAll(s, touchRadius);
                foreach(var c in col) {
                    
                    var go = Instantiate(circle, s, Quaternion.identity);
                    text.text = "osui " + i;

                    // if thing is draggable
                    //drags.Add(t.fingerId, rb);
                }
            }

            // if (drags.ContainsKey(t.fingerId)) 

        }
        //if(Input.GetKeyDown(KeyCode.Mouse0)) {
        //    var s = cam.ScreenToWorldPoint(Input.mousePosition);
        //    s.z = 0;
        //    var go = Instantiate(circle, s, Quaternion.identity);
        //    text.text = "osui ";
        //}
    }


}
