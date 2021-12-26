using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CupSpawner : MonoBehaviour
{
   // Prefab of cup you want to spawn
    public GameObject cup;
    public SceneController sceneController;

    private Vector3 initPos;
    private Transform parentTransform;

    // Reference to the number of drink in the scene
    public static int numDrinks = 1;

    private bool canSpawn = false;

    void Start()
    {
        if (cup != null)
        {
            initPos = cup.transform.position;
            parentTransform = this.transform.parent.transform;
        }
        sceneController = GameObject.FindObjectOfType<SceneController>();
    }

    public void OnMouseDown()
    {
        if (numDrinks <= 1)
        {
            SpawnCup();
        }
    }

    public void SpawnCup()
    {   
        // Create topping at mouse position
        cup.SetActive(true);
        GameObject copy = Instantiate(cup, initPos, Quaternion.identity);
        cup.SetActive(false);

        copy.name = cup.name;
        Vector3 localScale = cup.transform.localScale;
        
        // Reassign the parent of the GameObject and fix scale
        copy.transform.SetParent(parentTransform, true); 
        copy.transform.localScale = localScale;

        // Register cup to the scene controller
        sceneController.workstationItems.Add(copy);

        // Increase drink count
        numDrinks++;
    }
}
