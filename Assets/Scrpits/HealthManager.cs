using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public FloatValue playerCurrentHealth;
    public Slider SlidercurrentHealth;
    void Start()
    {
        SlidercurrentHealth.maxValue = playerCurrentHealth.RunTimeValue;
        

    }

    // Update is called once per frame
    void Update()
    {
        currentHealth();
    }
    public void currentHealth() {
        SlidercurrentHealth.value = playerCurrentHealth.RunTimeValue;
    }
}
