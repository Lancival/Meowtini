using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
   // Prefab of cup you want to spawn
    public GameObject cup;

    private Vector3 initPos;
    private Transform parentTransform;

    // Reference to the number of drink in the scene
    public static int numDrinks = 1;

    void Start()
    {
        if (cup != null)
        {
            initPos = cup.transform.position;
            parentTransform = this.transform.parent.transform;
        }
    }

    public void SpawnCup()
    {   
        // Create topping at mouse position
        GameObject copy = Instantiate(cup, initPos, Quaternion.identity);
        copy.name = cup.name;
        Vector3 localScale = cup.transform.localScale;
        
        // Reassign the parent of the GameObject and fix scale
        copy.transform.SetParent(parentTransform, true); 
        copy.transform.localScale = localScale;

        // Reassign the prefab to copy
        cup = copy;

        // Increase drink count
        numDrinks++;
    }
}
