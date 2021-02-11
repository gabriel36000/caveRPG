using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 directionToMove;
    public float lifeTime;
    private float lifeTimeSeconds;
    public Rigidbody2D myRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        lifeTimeSeconds = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if(lifeTimeSeconds <= 0) {
            Destroy(this.gameObject);
        }
    }
    public void Launch (Vector2 initial) {
        
        myRigidbody.velocity = initial * moveSpeed;
    }
    public void OnTriggerEnter2D(Collider2D col) {
        Destroy(this.gameObject);
    }
    
}
