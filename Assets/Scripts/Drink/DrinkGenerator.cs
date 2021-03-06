using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DrinkGenerator : MonoBehaviour
{
    [SerializeField] GameObject cup0;  // Corresponds to cup0 prefab
    [SerializeField] GameObject cup1;  // Corr: cup1 prefab
    [SerializeField] GameObject cup2;  // Corr: cup2 prefab
    [SerializeField] Image coldness; // Corr; Coldness object
    [SerializeField] SpriteList sprites;
    Drink result;
    public int cupShape;
    int numIce;
    private void Awake()
    {
        sprites = GameObject.Find("SpriteList").GetComponent<SpriteList>();
        GenerateDrink(6);  // Reference difficulty
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
                result.liquid = Drink.LiquidTypes.orange;
                break;
            case 1:
                result.liquid = Drink.LiquidTypes.purple;
                break;
            case 2:
                result.liquid = Drink.LiquidTypes.blue;
                break;
        }
        // Assign ice cubes based on difficulty
        numIce = 0;
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
        List<int> done = new List<int>();
        done.Clear();
        for (int i = 0; i < maxToppings; i++)  // NOTE: Currently assumes you can't have more than 1 of each topping
        {
            int index = Mathf.RoundToInt(Random.Range(0f, 1f) * 5); 
            while (done.Contains(index))
            {
                index = Mathf.RoundToInt(Random.Range(0f, 1f) * 5);
            }
            result.AddTopping(toppingList[index]);
            // Debug.Log(toppingList[index]);
            done.Add(index);
        }
    }

    private void Start()
    {
        // Display UI indicator for ice (coldness)
        switch (numIce)
        {
            case 0:
                coldness.enabled = false;
                break;
            case 1:
                coldness.enabled = true;
                coldness.sprite = sprites.GetSprite("cold1");
                break;
            case 2:
                coldness.enabled = true;
                coldness.sprite = sprites.GetSprite("cold2");
                break;
            case 3:
                coldness.enabled = true;
                coldness.sprite = sprites.GetSprite("cold3");
                break;
        }
    }
}



