using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryToHealBox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Trying to heal...");
        gameObject.GetComponent<DestructibleBox>().boxHealth += (1.0f * Time.deltaTime);
    }
}
