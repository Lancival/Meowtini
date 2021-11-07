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
        if (dropLocation.isOccupied && dropLocation.draggableReference != null)
        {   
            drink = dropLocation.draggableReference.GetComponent<Drink>();
            if (drink == null)
            {
                Debug.LogError("Expecting drink to be occupying a drop location.");
                return;
            } 

            // Iterate each base liquid color and calculate how much is currently stored in the shaker
            float sum = 0;
            foreach(var key in drink.liquids.Keys)
            {
                sum += drink.liquids[key];
            }   
            
            // If drink is at max capacity, don't add more
            if (sum >= drink.capacity)
                return;
            
            // Check if liquid already exists in dictionary
            if (drink.liquids.ContainsKey(liquid))
                drink.liquids[liquid] += dispenserRate;
            else
                drink.liquids[liquid] = dispenserRate;

            drink.curVolume = sum + dispenserRate;
        }
    }
}
