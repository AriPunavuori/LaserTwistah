using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public bool WallTopMirror;
    public bool WallBottomMirror;
    public bool WallLeftMirror;
    public bool WallRightMirror;

    void Start() {
        GameObject wTop = GameObject.Find("WallTop");
        SpriteRenderer srTop = wTop.GetComponentInChildren<SpriteRenderer>();

        GameObject wBottom = GameObject.Find("WallBottom");
        SpriteRenderer srBottom = wBottom.GetComponentInChildren<SpriteRenderer>();

        GameObject wLeft = GameObject.Find("WallLeft");
        SpriteRenderer srLeft = wLeft.GetComponentInChildren<SpriteRenderer>();

        GameObject wRight = GameObject.Find("WallRight");
        SpriteRenderer srRight = wRight.GetComponentInChildren<SpriteRenderer>();

        if (WallTopMirror) {
            srTop.tag = "Mirror";
        } else {
            srTop.tag = "Untagged";
        }
        if (WallBottomMirror) {
            srBottom.tag = "Mirror";
        } else {
            srBottom.tag = "Untagged";
        }
        if (WallLeftMirror) {
            srLeft.tag = "Mirror";
        } else {
            srLeft.tag = "Untagged";
        }
        if (WallRightMirror) {
            srRight.tag = "Mirror";
        } else {
            srRight.tag = "Untagged";
        }

        //Debug.Log("Top: " + srTop.tag);
        //Debug.Log("Bottom: " + srBottom.tag);
        //Debug.Log("Left: " + srLeft.tag);
        //Debug.Log("Right: " + srRight.tag);
    }
}
