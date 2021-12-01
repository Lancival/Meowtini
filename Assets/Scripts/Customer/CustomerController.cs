using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] int startingTimer = 60; // In seconds
    public int timer;

    [Header("Error Calculation")]
    [SerializeField] int errorThreshold = 20;

    [Header("UI Display")]
    [SerializeField] SpriteRenderer timerIcon;
    [SerializeField] IngredientList orangeDrink;
    [SerializeField] SpriteMask mask;
    private Color iconColor = Color.green;

    [Header("Cups")]
    [SerializeField] GameObject cup0;  // Corresponds to cup0 prefab
    [SerializeField] GameObject cup1;  // Corr: cup1 prefab
    [SerializeField] GameObject cup2;  // Corr: cup2 prefab
    Drink targDrink;

    [Header("Generator")]
    [SerializeField] DrinkGenerator gen;

    [Header("Sprite Hiding")]
    [SerializeField] SceneController sceneController;

    SpriteRenderer[] renderers;
    Image[] images;

    // Start is called before the first frame update
    void Start()
    {
        timer = startingTimer;
        // timerIcon.color = iconColor;
        // orangeDrink.DisplayIngredient("strawberry", 1);
        InvokeRepeating("HandleTimer", 0, 1);
        ChooseCup();
        // targDrink.PrintInfo();  // For debugging
        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        images = gameObject.GetComponentsInChildren<Image>();
    } 

    void ChooseCup()  
    {
        switch(gen.cupShape)
        {
            case 0:  // funnel (cup2)
                targDrink = cup2.GetComponent<Drink>();
                break;
            case 1:  // cylinder (cup1)
                targDrink = cup1.GetComponent<Drink>();
                break;
            case 2:  // flask (cup0)
                targDrink = cup0.GetComponent<Drink>();
                break;
        }
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

    private void Update()
    {
        // Hide Customer upon scene swap
        if (sceneController.isInWorkstation())
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
            foreach (var image in images)
            {
                image.enabled = false;
            }
        } else
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            foreach (var image in images)
            {
                image.enabled = true;
            }
        }

        // Update the heart display
        float yVal = Mathf.Lerp(timerIcon.transform.position.y + 0.13f, timerIcon.transform.position.y + 0.93f, (float) timer / startingTimer);
        mask.transform.position = new Vector3(timerIcon.transform.position.x, Mathf.Lerp(mask.transform.position.y, yVal, 1f), 0);
    }

    // Updates the time
    void HandleTimer()
    {
        timer--;
        if (timer == 0)
        {
            Debug.Log("ORDER FAILED");
            timerIcon.color = Color.black;
            GameManager.Instance.DrinkDenied(1);  // Calls GameManager to decrease satisfaction and reset the combo
            GameObject.Destroy(gameObject);
        }
    }
}
