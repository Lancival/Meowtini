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
            parentTransform = gameObject.transform.parent.transform;
        }
    }

    public void SpawnTopping()
    {   
        // Create topping at mouse position
        GameObject copy = Instantiate(topping, initPos, Quaternion.identity);
        copy.name = topping.name;
        
        // Reassign the parent of the GameObject
        copy.transform.parent = parentTransform;

        // Reassign the prefab to copy
        topping = copy;
    }
}
