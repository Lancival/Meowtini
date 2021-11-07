using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [Header("Settings")]    
    [Tooltip("Name of liquid color")]
    [SerializeField] private string liquid;
    [SerializeField] private float dispenserRate;

    private Shaker shaker;

    void Start()
    {
        shaker = FindObjectOfType<Shaker>();    
    }

    public void DispenseLiquid()
    {
        if (shaker == null)
        {
            Debug.LogError("Expecting shaker to be occupying a drop location.");
            return;
        } 

        // Iterate each base liquid color and calculate how much is currently stored in the shaker
        float sum = 0;
        foreach(var key in shaker.liquids.Keys)
        {
            sum += shaker.liquids[key];
        }   
        
        // If shaker is at max capacity, don't add more
        if (sum >= shaker.capacity)
            return;
        
        // Check if liquid already exists in dictionary
        if (shaker.liquids.ContainsKey(liquid))
            shaker.liquids[liquid] += dispenserRate;
        else
            shaker.liquids[liquid] = dispenserRate;

        shaker.curVolume = sum + dispenserRate;
    }
}
