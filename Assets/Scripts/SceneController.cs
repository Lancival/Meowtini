using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] Canvas workstation;
    [SerializeField] Canvas counter;
    [SerializeField] SpriteRenderer background;
    [SerializeField] SpriteList sprites;


    public List<GameObject> workstationItems;   
    private bool inWorkstation;

    void Awake()
    {
        workstationItems = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject items = GameObject.Find("Workstation Items");
        foreach (Transform child in items.transform)
        {
            workstationItems.Add(child.gameObject);
        }
        workstation.enabled = false;
        inWorkstation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (workstation.enabled)
        {
            inWorkstation = true;
            background.sprite = sprites.GetSprite("workstation");
            foreach (GameObject item in workstationItems)
            {
                if (item.tag != "Inactive")
                    item.SetActive(true);
            }

        } else
        {
            inWorkstation = false;
            foreach (GameObject item in workstationItems)
            {
                // Keep drink enabled if it is picked up
                if (item.tag == "Drink")
                {
                    DraggableDrink drink = item.GetComponent<DraggableDrink>();
                    if (drink.isDragging)
                    {
                        continue;
                    }
                }
                    item.SetActive(false);
            }
            background.sprite = sprites.GetSprite("counter");
        }
    }

    // Checks if current scene is workstation
    public bool isInWorkstation()
    {
        return inWorkstation;
    }
}