using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] int startingTimer = 60; // In seconds
    public int timer;

    [Header("Error Calculation")]
    [SerializeField] int errorThreshold = 30;

    [Header("UI Display")]
    [SerializeField] SpriteRenderer timerIcon;
    [SerializeField] IngredientList ingredientList;
    private Color iconColor = Color.green;

    private Drink targDrink;


    // Start is called before the first frame update
    void Start()
    {
        timer = startingTimer;
        timerIcon.color = iconColor;
        InvokeRepeating("HandleTimer", 0, 1); 
    } 

    void DisplayRecipe()
    {
        IngredientList ing = Object.Instantiate(ingredientList);
        ing.DisplayIngredient("ice", 1);
    }

    // Judges if the candidate matches the target sufficiently
    bool EvaluateDrink(Drink candidate, Drink target)
    {
        /*
         * This code has not been tested, but in theory this should be a functional method to account for error.
         */
        int errorValue = 0;

        // Compare candidate's topping list to the target, adding errorValue when discrepancies occur
        foreach(string topping in candidate.toppings.Keys)
        {
            if (target.toppings.ContainsKey(topping))
            {
                int dif = candidate.toppings[topping] - target.toppings[topping];
                errorValue += Mathf.Abs(dif) * 2;  // Add error score of 2 for each extra/missing topping

            } else
            {
                int count = candidate.toppings[topping];
                errorValue += Mathf.Abs(count) * 2;
            }
        }
        // Accounts for missing toppings from the candidate
        foreach (string topping in target.toppings.Keys)
        {
            if (!candidate.toppings.ContainsKey(topping))
            {
                int count = target.toppings[topping];
                errorValue += Mathf.Abs(count) * 2;
            }
        }

        // Liquid check
        float tolerance = 20f * Mathf.Pow(0.8f, 0);  // 0 should be set to the difficulty variable in the future
        
        foreach (string liquid in candidate.liquids.Keys)
        {
            if (target.toppings.ContainsKey(liquid))
            {
                float dif = Mathf.Abs(candidate.liquids[liquid] - target.liquids[liquid]);
                if (dif > tolerance)
                {
                    dif -= tolerance;
                    errorValue += Mathf.CeilToInt(dif / tolerance);  // Gain 1 errorValue for each tolerance away from the target answer
                }
            }
            else
            {
                errorValue += 5;  // Gain 5 errorValue for adding wrong liquid
            }
        }
        // Accounts for missing liquids from the candidate
        foreach (string liquid in target.liquids.Keys)
        {
            if (!candidate.liquids.ContainsKey(liquid))
            {
                errorValue += 5;  // Gain 5 errorValue for missing a liquid
            }
        }
        return errorValue < errorThreshold;
    }

    // Handle drink submission - Call this function when a drink is delivered to the customer
    void CheckDrink(Drink candidate, Drink target)
    {
        if (EvaluateDrink(candidate, target))  // Drink acceptable
        {
            GameManager.Instance.DrinkAccepted(1);
        } else  // Drink unacceptable
        {
            GameManager.Instance.DrinkDenied(1);
        }
        GameObject.Destroy(gameObject);
    }

    // Updates the time
    void HandleTimer()
    {
        timer--;
        if (timer > startingTimer / 3 * 2) // Transition from green to yellow
        {
            // iconColor = new Vector4(iconColor.r + 0.05f, iconColor.g, iconColor.b, iconColor.a);
            iconColor = Color.Lerp(Color.yellow, Color.green, 0.05f * (timer - startingTimer / 3 * 2));  // Probably a better way to do it
        }
        else if (timer == startingTimer / 3 * 2) // Set to yellow
        {
            iconColor = Color.yellow;
        }
        else if (timer > startingTimer / 3)  // Transition from yellow to red
        {
            iconColor = Color.Lerp(Color.red, Color.yellow, 0.05f * (timer - startingTimer / 3));
        } 
        else if (timer == startingTimer / 3)  // Set to red
        {
            iconColor = Color.red;
        } 
        else  // Transition from red to (nearly) black
        {
            iconColor = Color.Lerp(Color.Lerp(Color.black, Color.red, 0.2f) , Color.red, 0.05f * (timer));
        }
        timerIcon.color = iconColor;
        if (timer == 0)
        {
            Debug.Log("ORDER FAILED");
            timerIcon.color = Color.black;
            GameManager.Instance.DrinkDenied(1);  // Calls GameManager to decrease satisfaction and reset the combo
            GameObject.Destroy(gameObject);
        }
    }
}
