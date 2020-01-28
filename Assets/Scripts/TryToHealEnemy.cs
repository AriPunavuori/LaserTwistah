using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryToHealEnemy : MonoBehaviour
{
    public float enemyHealingRate;

    void Update() {
        //Debug.Log("Trying to heal...");
        // We'll try to heal the enemy a bit in every frame.
        if (GetComponent<DestructibleEnemy>().enemyHealth < GetComponent<DestructibleEnemy>().enemyMaxHealth) {
            GetComponent<DestructibleEnemy>().enemyHealth += (enemyHealingRate * Time.deltaTime);
        }
    }
}
