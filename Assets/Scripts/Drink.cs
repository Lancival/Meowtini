using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    /// <summary>
    /// Drink creation steps: choose cup shape, pour alcohol, choose liquid, add ice, shake, pour liquid, add toppings
    /// </summary>


    public Dictionary<string, float> liquids;  // String is the liquid name, float is the amount
    public Dictionary<string, int> toppings;  // String is topping name, int is number of said topping
    public int numIce;

    public float capacity; // Maximum volume of liquid drink can hold
    public float curVolume = 0; // Current volume of the drink

    public enum CupShapes
    {
        funnel,
        cylinder,
        flask
    }
    public CupShapes cupShape;


    void Start()
    {
        liquids = new Dictionary<string, float>();
        liquids.Clear();
        toppings = new Dictionary<string, int>();
        toppings.Clear();
    }

    public void SetCupShape(CupShapes shape)
    {
        cupShape = shape;
    }

    public void SetCupShape(int shape)
    {
        cupShape = (CupShapes) shape; 
    }

    void Update()
    {

    }
}
