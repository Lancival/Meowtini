using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [Header("Shaker Capacity Settings")]
    public float capacity; // Maximum volume of liquid shaker can hold
    public float curVolume = 0; // Current volume of liquid stored in the shaker
    public Dictionary<string, float> liquids;  // String is the liquid name, float is the amount
    public int numIceCubes = 0; // Number of ice cubes 

    [Header("Timer Settings")]
    [SerializeField] private Dispenser dispenser;
    [SerializeField] private float timer;
    [SerializeField] private float period = 0.5f;
    [SerializeField] private bool startTimer = false;
    
    void Start() {
        liquids = new Dictionary<string, float>();
        liquids.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ice")
        {
            Debug.Log("Feature not implemented");
        }
        if (other.gameObject.tag == "Dispenser")
        {
            dispenser = other.gameObject.GetComponent<Dispenser>();
            Animator anim = other.gameObject.GetComponent<Animator>();
            anim.SetBool("Pour", true);
            dispenser.ActivatePourAnimation();
            startTimer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Dispenser")
        {
            Animator anim = other.gameObject.GetComponent<Animator>();
            anim.SetBool("Pour", false);
            timer = period;
            dispenser.DeactivatePourAnimation();
            dispenser = null;
            startTimer = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Dispenser")
        {
            if (dispenser != null && startTimer)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    dispenser.DispenseLiquid();
                    timer = period;
                }      
            }
        }    
    }
}
