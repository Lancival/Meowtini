using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    const float MAX_SATISFACTION = 100f;

    [Header("Difficulty Settings")]
        // Not sure whether this should be a discrete value
        // or a some continuous variable
        [Tooltip("Difficulty multiplier which should scale according to time")]
        [SerializeField] private float diffMultiplier;

    [Header("Universal Timer")]
        [Tooltip("Timer which starts at the beginning of the game")]
        [SerializeField] private float timer = 0.0f;
        [Tooltip("How many seconds have passed since the beginnig of the game")]
        [SerializeField] private int secondsElapsed;
    
    [Header("Combo counter")]
        [Tooltip("Drinks correctly served in a row")]
        [SerializeField] private int combo = 0;

    [Header("Total Satisfaction")]
        [Tooltip("Satisfication level (percent)")]
        [SerializeField] private float totalSatisfaction = 50f;

    [Header("Spawning")]
    private Dictionary<int, bool> locations;
    [SerializeField] GameObject customer;

    private void Start()
    {
        locations = new Dictionary<int, bool>();  // Key is the x coordinate, value is whether it's empty
        locations.Add(-5, true);
        locations.Add(-1, true);
        locations.Add(3, true);
        SpawnCustomer();
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        secondsElapsed = (int)(timer % 60);
        
    }

    // Find a suitable position to spawn the customer
    void SpawnCustomer()
    {
        int[] ls = new int[3];
        locations.Keys.CopyTo(ls, 0);
        int index = Mathf.RoundToInt(Random.Range(0, locations.Keys.Count - 1));
        while (!locations[ls[index]])
        {
            index = Mathf.RoundToInt(Random.Range(0, locations.Keys.Count - 1));
        }
        Instantiate(customer, new Vector3(ls[index], 1.2f, 0), Quaternion.identity);
        locations[ls[index]] = false;
    }
    // Indicates a customer has been removed at given x coordinate
    public void updateRemovedCustomer(int x)
    {
        if (locations.ContainsKey(x))
        {
            locations[x] = true;
        } else
        {
            Debug.LogError("Invalid x");
        }
    }

    #region Getters

    public float GetSatisfaction()
    {
        return totalSatisfaction;
    }

    #endregion

    #region Serving Drinks
    public void DrinkAccepted(int amount)
    {
        IncreaseComboCounter();
        IncreaseSatisfaction(amount);
    }
    public void DrinkDenied(int amount)
    {   
        ResetComboCounter();
        DecreaseSatisfaction(amount);
    } 
    #endregion

    # region Combo Functions
    private void ResetComboCounter() { combo = 0; }
    private void IncreaseComboCounter() { combo++; }
    #endregion

    # region Satisfaction Functions
    private void IncreaseSatisfaction(int amount) {
        if (totalSatisfaction + amount > MAX_SATISFACTION)
            totalSatisfaction = MAX_SATISFACTION;
        else 
            totalSatisfaction += amount; 
    }
    private void DecreaseSatisfaction(int amount) { totalSatisfaction -= amount; }
    #endregion 
}
