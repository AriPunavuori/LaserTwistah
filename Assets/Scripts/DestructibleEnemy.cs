using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEnemy : MonoBehaviour, IDamageable {

    public float enemyHealth = 100f;
    public float enemyMinHealth = 0f;
    public float enemyMaxHealth = 100f;

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
        // Enemy get's some damage.
        enemyHealth -= damageAmount;
        GetComponentInChildren<SpriteRenderer>().material.SetFloat("_FlashAmount", (enemyMaxHealth - enemyHealth) / enemyMaxHealth);
        //audioBoxTakesHits.Play();
        isEnemyDestroyed(enemyHealth);
    }

    void isEnemyDestroyed(float health) {
        // If health goes too low we'll remove enemy.
        if (health <= enemyMinHealth) {
            //audioBoxVanishes.Play();
            Destroy(gameObject, 0.5f);
        }
        //if (health >= enemyMaxHealth) {
        //    health = enemyMaxHealth;
        //}
    }
}
