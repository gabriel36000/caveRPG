using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        powerUpSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player") && !col.isTrigger) {
            playerInventory.coins += 1;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
        
    }

}
