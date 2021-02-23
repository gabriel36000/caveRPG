using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light sun;
    void Start()
    {
        StartCoroutine(Sunset());
    }

    IEnumerator Sunset() {
        while(sun.intensity > 0.5f) {
            sun.intensity -= 0.0001f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Sunrise());
        StopCoroutine(Sunset());
    }
     IEnumerator Sunrise() {
        while (sun.intensity < 0.5f) {
            sun.intensity += 0.0001f;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Sunset());
        StopCoroutine(Sunrise());
    }
    
}
