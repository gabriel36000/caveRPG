using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
            col.GetComponent<ObjectDestroy>().Destroy();
        }
            if (col.gameObject.CompareTag("enemy") || col.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = col.GetComponent<Rigidbody2D>();
            if(hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (col.gameObject.CompareTag("enemy") && col.isTrigger) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    col.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (col.gameObject.CompareTag("Player")) {
                    if(col.GetComponent<PlayerMovement>().currentState != PlayerState.stagger) {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        col.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                    
                }
              
            }
        }
    }
}
