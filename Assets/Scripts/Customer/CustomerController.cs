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
    public Drink targDrink;

    [Header("Cats")]
    [SerializeField] SpriteRenderer catRenderer; // Corr: Cat prefab
    [SerializeField] Sprite star;
    [SerializeField] Sprite strawberry;
    [SerializeField] Sprite sunflower;
    [SerializeField] Sprite sweater;
    [SerializeField] Animator catAnim;

    [Header("Generator")]
    [SerializeField] DrinkGenerator gen;

    [Header("Sprite Hiding")]
    [SerializeField] public SceneController sceneController;

    [Header("Drink Score")]
    [SerializeField] public int DRINK_ACCEPTED_SCORE = 10;
    [SerializeField] public int DRINK_DENIED_SCORE = 10;

    SpriteRenderer[] renderers;
    Image[] images;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Customer";
        timer = startingTimer;
        InvokeRepeating("HandleTimer", 0, 1);
        ChooseCat();
        ChooseCup();
        // targDrink.PrintInfo();  // For debugging
        sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();

        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        images = gameObject.GetComponentsInChildren<Image>();
    } 

    // Chooses a Cup shape randomly to initialize 
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

    // Chooses a cat to display
    void ChooseCat()
    {
        int rand = Mathf.RoundToInt(Random.RandomRange(0f, 3f)); // Gens a random integer between 0 and 3 inclusive
        switch (rand)
        {
            case 0:
                catRenderer.sprite = star;
                break;
            case 1:
                catRenderer.sprite = sunflower;
                break;
            case 2:
                catRenderer.sprite = strawberry;
                break;
            case 3:
                catRenderer.sprite = sweater;
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
        // float tolerance = 20f * Mathf.Pow(0.8f, 0);  // 0 should be set to the difficulty variable in the future
        if (candidate.liquid != target.liquid) // If wrong liquid type
            errorValue += 19;
        
        return errorValue < errorThreshold;
    }

    // Handle drink submission - Call this function when a drink is delivered to the customer
    public void CheckDrink()
    {
        Drink candidate = Drink.FindClosestDrink(gameObject.transform.position);
        Drink target = targDrink;
        Debug.Log("Checking");
        if (EvaluateDrink(candidate, target))  // Drink acceptable
        {
            GameManager.Instance.DrinkAccepted(DRINK_ACCEPTED_SCORE);
            GameManager.Instance.updateRemovedCustomer(Mathf.RoundToInt(transform.position.x));
            GameManager.Instance.SpawnCustomer();
            GameObject.Destroy(gameObject);
        }
        else  // Drink unacceptable
        {
            // GameManager.Instance.DrinkDenied(10);
        }
        
    }

    public void CheckDrink(Drink candidate, Drink target)
    {
        if (sceneController.isInWorkstation())
            return;
        Debug.Log("Checking");
        if (EvaluateDrink(candidate, target))  // Drink acceptable
        {
            GameManager.Instance.DrinkAccepted(DRINK_ACCEPTED_SCORE);
            GameManager.Instance.updateRemovedCustomer(Mathf.RoundToInt(transform.position.x));
            GameManager.Instance.SpawnCustomer();
            GameObject.Destroy(gameObject);
        } else  // Drink unacceptable
        {
            GameManager.Instance.DrinkDenied(DRINK_DENIED_SCORE);
        }
    }

    bool prevState = false;  // starts outside workstation

    

    private void Update()
    {
        // Hide Customer upon scene swap
        if (sceneController.isInWorkstation())
        {
            DisableColliders();

            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
            foreach (var image in images)
            {
                image.enabled = false;
            }
        }
        else if (prevState != sceneController.isInWorkstation())
        {
            EnableColliders();

            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            foreach (var image in images)
            {
                image.enabled = true;
            }
        }

        prevState = sceneController.isInWorkstation();  // Update previous state

        // Update the heart display
        float yVal = Mathf.Lerp(timerIcon.transform.position.y + 0.13f, timerIcon.transform.position.y + 0.93f, (float) timer / startingTimer);
        mask.transform.position = new Vector3(timerIcon.transform.position.x, Mathf.Lerp(mask.transform.position.y, yVal, 1f), 0);
    }

    // Disables the colliders when the scene swaps
    private void DisableColliders()
    {
        cup0.GetComponent<Collider2D>().enabled = false;
        cup1.GetComponent<Collider2D>().enabled = false;
        cup2.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    // Re-enable colliders on scene swap
    private void EnableColliders ()
    {
        cup0.GetComponent<Collider2D>().enabled = true;
        cup1.GetComponent<Collider2D>().enabled = true;
        cup2.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    // Updates the time
    void HandleTimer()
    {
        timer--;
        if (timer == 0)
        {
            Debug.Log("ORDER FAILED");
            timerIcon.color = Color.black;
            GameManager.Instance.DrinkDenied(DRINK_DENIED_SCORE);  // Calls GameManager to decrease satisfaction and reset the combo
            GameManager.Instance.updateRemovedCustomer(Mathf.RoundToInt(transform.position.x));
            GameManager.Instance.SpawnCustomer();
            GameObject.Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Customer destroyed");
        catAnim.SetTrigger("Fade");
    }
}
