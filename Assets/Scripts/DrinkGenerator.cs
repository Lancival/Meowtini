using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGenerator : MonoBehaviour
{
    public static Drink GenerateDrink(int difficulty)
    {
        List<string> toppingList = new List<string>() { "cherry", "strawberry", "olive", "mint", "cream", "lime" };
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
        for (int i = 0; i < maxToppings; i++)  // NOTE: Currently assumes you can't have more than 1 of each topping
        {
            int index = Mathf.RoundToInt(Random.Range(0f, 1f));
            result.toppings.Add(toppingList[index], 1);
            toppingList.RemoveAt(index);
        }

        return result;
    }
}
