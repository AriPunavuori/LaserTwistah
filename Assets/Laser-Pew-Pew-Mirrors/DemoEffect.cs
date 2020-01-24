using UnityEngine;
using System.Collections;

public class DemoEffect : MonoBehaviour {
    Vector3 startPosition;
    void Awake() {
        startPosition = transform.position;
    }
	void Update () {
        transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * 2.0f) * 2.0f;
	}
}
