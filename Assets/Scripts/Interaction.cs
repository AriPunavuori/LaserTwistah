using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour{
    [SerializeField]
    bool draggable;

    public void MakeDraggable(bool d) {
        draggable = d;
    }
    public bool IsDraggable() {
        return draggable;
    }
}
