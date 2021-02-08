using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public Signal context;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !col.isTrigger) {
            context.Raise();
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player") && !col.isTrigger) {
            context.Raise();
            playerInRange = false;
            
        }
    }
}
