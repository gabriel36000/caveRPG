using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public GameObject coin;
    public LootTable thisLoot;

    private void Awake() {
        health = maxHealth.initialValue;
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime) {
        if (myRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
           currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    private void TakeDamage( float damage) {
        health -= damage;
        if(health <= 0) {
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
        }
    }
    public void DeathEffect() {
        if(deathEffect != null) {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, .5f);
        }
    }
    private void MakeLoot() {
        if(thisLoot != null) {
            PowerUp current = thisLoot.LootPowerup();
            if(current != null){
                Instantiate(current.gameObject, transform.position + transform.up * -0.8f, Quaternion.identity);
                
            }
        }
    }
    

    
}
