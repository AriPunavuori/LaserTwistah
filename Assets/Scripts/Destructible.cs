using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{

    public float Health = 100f;
    public float MinHealth = 0f;
    public float MaxHealth = 100f;
    public bool Healable;
    bool isTakingDamage;
    public float HealingAmount; // per 1 second
    public bool isFriendly;
    GameManager gm;

    private void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    public void DamageIt(float damageAmount) {
        //Debug.Log("You damaged it.");
        // Enemy get's some damage.
        Health -= damageAmount * Time.deltaTime;
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", (MaxHealth - Health) / MaxHealth);
        isDestroyed(Health);
        isTakingDamage = true;
    }

    void isDestroyed(float health) {
        // If health goes too low we'll remove enemy.
        if(health <= MinHealth) {
            //audioVanishes.Play();
            AudioFW.Play("TargetExplodes");

                if(isFriendly) {
                    gm.Friendykilled();
                    print("Killed Freindly");
                } else {
                    gm.EnemyKilled();
                    Destroy(gameObject);
                }
        }
        //if (health >= enemyMaxHealth) {
        //    health = enemyMaxHealth;
        //}
    }

    void Update() {
        //Debug.Log("Trying to heal...");
        // We'll try to heal target a bit in every frame.
        if(Healable && !isTakingDamage) {
            if(Health < MaxHealth) {
                Health += (HealingAmount * Time.deltaTime);
                GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", (MaxHealth - Health) / MaxHealth);
            }
        }

        isTakingDamage = false;
        //audioTakesHits.Stop();
    }
}
