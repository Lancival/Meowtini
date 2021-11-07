using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("DropLocation where Draggable objects can go.")]
    [SerializeField] private DropLocation dropLocation;
    
    [Tooltip("Name of liquid color")]
    [SerializeField] private string liquid;

    //[Tooltip("Levers to dispense liquid.")]
    //[SerializeField] private Lever[] levers;


    [SerializeField] private float dispenserRate;

    private Drink drink;

    // Gets called by OnMouseDown() in Lever.cs
    public void DispenseLiquid()
    {
        if (dropLocation.isOccupied)
        {
            if (drink == null)
            {
                Debug.LogError("Expecting drink to be occupying a drop location.");
            } 

            // Iterate each base liquid color and calculate how much is currently stored in the shaker
            float sum = 0;
            foreach(KeyValuePair<string, float> entry in drink.liquids)
            {
                sum += entry.Value ;
            }   

            if (sum >= drink.capacity)
                return;
            
            drink.liquids[liquid] += dispenserRate;
            drink.curVolume = sum + dispenserRate;
        }
        
    	Debug.Log("Liquid dispensing functionality not yet implemented.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Drink" && !dropLocation.isOccupied)
        {
            drink = collision.gameObject.GetComponent<Drink>();
        }
    }
}
