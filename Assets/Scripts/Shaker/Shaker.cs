using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Shaker Cap")]
    [SerializeField] private GameObject shakerCap;
    public bool canShake = false;
    
    public bool canDropIce = false;

    void Start() {
        liquids = new Dictionary<string, float>();
        liquids.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ice")
        {
            canDropIce = true;
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
        if (other.gameObject.tag == "Ice")
        {
            canDropIce = false;
        }
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

    public string GetMajorityColor()
    {
       return liquids.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    }

    public void DeactivateShakerCap()
    {
        if (shakerCap == null)
            Debug.LogError("Forgot to assign shaker cap to shaker!");
        else
            shakerCap.SetActive(false);
    }

    public void ActivateShakerCap()
    {
        if (shakerCap == null)
            Debug.LogError("Forgot to assign shaker cap to shaker!");
        else
            shakerCap.SetActive(true);
    }

    public void ActivateCupAnimation()
    {
        // Get closest drink
        Drink drink = Drink.FindClosestDrink(this.transform.position);
        
        if (drink != null)
        {
            // Set volume back to 0
            curVolume = 0;
            
            string color = GetMajorityColor();

            if (color == "Purple")
            {
                drink.liquid = Drink.LiquidTypes.purple;
            }
            else if (color == "Orange")
            {
                drink.liquid = Drink.LiquidTypes.orange;
            }
            else if (color == "Clear")
            {
                drink.liquid = Drink.LiquidTypes.clear;
            }

            else
            {
                drink.liquid = Drink.LiquidTypes.blue;
            }

            Animator liquid = drink.GetComponentInChildren<Animator>();
            if (liquid == null)
            {
                Debug.LogError("Expected animator for the liquid GameObject");
            }
            liquid.SetTrigger("Fill");
        }
    }
}
