using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGenerator : MonoBehaviour
{
    List<string> toppings = new List<string>();
    private void Start()
    {
        toppings.Add("cherry");
        toppings.Add("strawberry");
        toppings.Add("olive");
        toppings.Add("mint");
        toppings.Add("cream");
        toppings.Add("lime");
    }

    public static Drink GenerateDrink(int difficulty)
    {
        Drink result = new Drink();
        // Set cup shape
        int state = Mathf.RoundToInt(Random.Range(0f, 2f));  // Random int from 0-2 inclusive
        result.SetCupShape(state);
        // Set liquid requirement
        state = Mathf.RoundToInt(Random.Range(0f, 2f));  // Rerolls the int
        switch (state)  // Assign random liquid
        {
            case 0:
                result.liquids.Add("orange", 0f);
                break;
            case 1:
                result.liquids.Add("purple", 0f);
                break;
            case 2:
                result.liquids.Add("blue", 0f);
                break;
        }
        // Assign ice cubes based on difficulty
        if (difficulty > 3)
        {
            result.numIce = Mathf.RoundToInt(Random.Range(0f, 3f));
        } else if (difficulty > 1)
        {
            result.numIce = Mathf.RoundToInt(Random.Range(0f, 2f));
        } else
        {
            result.numIce = Mathf.RoundToInt(Random.Range(0f, 1f));
        }
        // Assign toppings based on difficulty
        int maxToppings = difficulty / 3;


        return result;
    }
}
