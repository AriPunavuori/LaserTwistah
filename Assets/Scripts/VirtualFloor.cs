using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualFloor : MonoBehaviour
{
    Rigidbody2D rb;
    public float floorHeight = -2f;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.y < floorHeight) {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }

    }
}
