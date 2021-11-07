using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    /// <summary>
    /// This class is just a placeholder that stores some data types in order to implement checking a drink's accuracy
    /// through CustomerController
    /// </summary>
    
    public Dictionary<string, float> liquids;  // String is the liquid name, float is the amount (units undecided)
    public Dictionary<string, int> toppings;  // String is topping name, int is number of said topping

    public float capacity {get; private set;} // Volume of liquid drink can hold
    public float curVolume {get; set;}


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
