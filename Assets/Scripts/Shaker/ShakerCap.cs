using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerCap : MonoBehaviour
{   
    // Requires reference to shaker
    [SerializeField] private Shaker shaker;
    
    // The minimum amount of liquid in the shaker to trigger animation
    [SerializeField] private int minVolume;
    
    [SerializeField] private float shakeAnimationTime;
    
    void Start()
    {
        shaker = GameObject.FindWithTag("Shaker").GetComponent<Shaker>();
    }

    private void OnMouseDown() {
        if (shaker.canShake)
        {
            StartCoroutine(ShakerAnimationCoroutine());    
        }
    }

    IEnumerator ShakerAnimationCoroutine()
    {
        if (shaker.curVolume >= minVolume)
        {
            shaker.canShake = false;
            shaker.gameObject.GetComponent<Animator>().SetTrigger("Shake");
            yield return new WaitForSeconds(shakeAnimationTime);
            shaker.canShake = true;
        }
    }
}
