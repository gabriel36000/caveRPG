using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public FloatValue playerCurrentHealth;
    public Slider SlidercurrentHealth;
    public float healthRegen = 0.1f;
   
    void Start()
    {
        SlidercurrentHealth.maxValue = playerCurrentHealth.RunTimeValue;
        StartCoroutine(RegainHealthOvertTime());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        currentHealth();
        
    }
    public void currentHealth() {
        SlidercurrentHealth.value = playerCurrentHealth.RunTimeValue;
    }
    private IEnumerator RegainHealthOvertTime() {
        while (true) {
            if(playerCurrentHealth.RunTimeValue < 6f) {
                playerCurrentHealth.RunTimeValue += healthRegen;
                yield return new WaitForSeconds(1);
            }
            else {
                yield return null;
            }
        }
        
    }
}
