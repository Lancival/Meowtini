using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{   
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
        [Tooltip("Satisfication level (lives)")]
        [SerializeField] private int totalSatisfaction = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        secondsElapsed = (int)(timer % 60);
    }

    # region Combo Functions
    public void ResetComboCounter() { combo = 0; }
    public void IncreaseComboCounter() { combo++; }
    #endregion

    # region Satisfaction Functions
    public void IncreaseSatisfaction(int amount) { totalSatisfaction += amount; }
    public void DecreaseSatisfaction(int amount) { totalSatisfaction -= amount; }
    #endregion 
    
}
