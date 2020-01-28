using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour, IDamageable {

    public float boxHealth = 100f;

    //public AudioSource audioBoxVanishes;
    //public AudioSource audioBoxTakesHits;

    /*
    public void HitSomething(GameObject whoHit) {
        // Box hits/bounces into something and takes minor damage.
        Debug.Log(gameObject.name + ": " + whoHit.name + " hit me!");
        boxHealth -= 1f;
        if (whoHit.tag == "hitsomething") {
            ;
        } else {
            ;
        }

        isBoxDestroyed(boxHealth);
    }
    */

    public void DamageIt(float damageAmount) {
        //Debug.Log("You damaged it.");
        boxHealth -= damageAmount;
        //audioBoxTakesHits.Play();
        isBoxDestroyed(boxHealth);
    }

    //void OnCollisionEnter(Collision collision) {
    //    HitSomething(collision.gameObject);
    //}

    void isBoxDestroyed(float health) {
        if (health <= 0) {
            //audioBoxVanishes.Play();
            Destroy(gameObject, 0.5f);
        }
        if (health >= 100) {
            health = 100f;
        }
    }
}
