using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingSpawner : MonoBehaviour
{
    // Prefab of topping you want to spawn
    public GameObject topping;

    private Vector3 initPos;
    private Transform parentTransform; 

    void Start()
    {
        if (topping != null)
        {
            initPos = topping.transform.position;
            parentTransform = GameObject.FindObjectOfType<Topping>().transform.parent.transform;
        }
    }

    public void SpawnTopping()
    {   
        // Create topping at mouse position
        GameObject copy = Instantiate(topping, initPos, Quaternion.identity);
        copy.name = topping.name;
        Vector3 localScale = topping.transform.localScale;
        
        // Reassign the parent of the GameObject and fix scale
        copy.transform.SetParent(parentTransform, true); 
        copy.transform.localScale = localScale;

        // Reassign the prefab to copy
        topping = copy;
    }
}
