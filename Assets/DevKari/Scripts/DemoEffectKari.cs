using UnityEngine;
using System.Collections;

public class DemoEffectKari : MonoBehaviour {
    
    Vector3 startPosition;
    public float DemoSpeed;

    void Awake() {
        startPosition = transform.position;
    }
	void Update () {
        //transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * 2.0f) * 2.0f;
        transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * DemoSpeed) * 2.0f;
	}
}
