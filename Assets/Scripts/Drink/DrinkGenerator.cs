using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGenerator : MonoBehaviour
{
    [SerializeField] GameObject cup0;  // Corresponds to cup0 prefab
    [SerializeField] GameObject cup1;  // Corr: cup1 prefab
    [SerializeField] GameObject cup2;  // Corr: cup2 prefab
    Drink result;
    public int cupShape;

    private void Awake()
    {
        GenerateDrink(3);  // Reference difficulty
    }
    public void GenerateDrink(int difficulty)
    {
        List<string> toppingList = new List<string>() { "cherry", "strawberry", "olive", "mint", "cream", "lime" };

        // Set cup shape & activate cup
        int state = Mathf.RoundToInt(Random.Range(0f, 2f));  // Random int from 0-2 inclusive
        cupShape = state;
        switch (cupShape)
        {
            case 0:  // funnel (cup2)
                cup2.SetActive(true);
                result = cup2.GetComponent<Drink>();
                break;
            case 1:  // cylinder (cup1)
                cup1.SetActive(true);
                result = cup1.GetComponent<Drink>();
                break;
            case 2:  // flask (cup0)
                cup0.SetActive(true);
                result = cup0.GetComponent<Drink>();
                break;
        }
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
        int numIce = 0;
        if (difficulty > 3)
        {
            numIce = Mathf.RoundToInt(Random.Range(0f, 3f));
        } else if (difficulty > 1)
        {
            numIce = Mathf.RoundToInt(Random.Range(0f, 2f));
        } else
        {
            numIce = Mathf.RoundToInt(Random.Range(0f, 1f));
        }
        result.toppings.Add("ice", numIce);
        // Assign toppings based on difficulty
        int maxToppings = difficulty / 3;
        for (int i = 0; i < maxToppings; i++)  // NOTE: Currently assumes you can't have more than 1 of each topping
        {
            int index = Mathf.RoundToInt(Random.Range(0f, 1f));
            result.toppings.Add(toppingList[index], 1);
            toppingList.RemoveAt(index);
        }
    }
}
