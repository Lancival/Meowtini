using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDrink : MonoBehaviour
{
    // Requires a spawner
	public CupSpawner spawner;

	private Camera cam;

    [SerializeField] private bool onTrash;
    [SerializeField] private bool onCounter;
    [SerializeField] private bool onCustomer;
    [SerializeField] private bool onButton;

    public bool canDrag {private set; get;}

    [SerializeField] private bool isDragging = false;

    // reference to shaker
    Shaker shaker;
    
    // Should snap to these coordinates when onCounter is true
    public Transform counterLocation;

    // All draggable drinks active in the scene
    public static HashSet<DraggableDrink> Pool = new HashSet<DraggableDrink>();

    void OnEnable()
    {
        DraggableDrink.Pool.Add(this);
    }

    void OnDisable()
    {
        DraggableDrink.Pool.Remove(this);
    }
    
    void Awake()
	{
		cam = Camera.main;
        onTrash = false;
        onCounter = false;
        onCustomer = false;
    }

    void Start()
    {
        shaker = GameObject.FindWithTag("Shaker").GetComponent<Shaker>();
        isDragging = true;
        if (CanDrag())
            canDrag = true;
        this.gameObject.tag = "Drink";
    }

    // Returns true if we can drag this instance of a drink, false otherwise
    private bool CanDrag()
    {
        foreach(DraggableDrink drink in Pool)
        {
            if (drink != this && drink.canDrag)
                return false;
        }
        return true;
    }

    // When the cup is first clicked, we'll spawn a duplicate behind it.
	void OnMouseDown() {
        // Check if it's in the trash
        if (onTrash)
        {
            Destroy(this.gameObject);
        }

        // Check if it's on the counter
        if (onCounter)
        {
            // Enable the drink script
            Drink drink = GetComponent<Drink>();
            drink.enabled = true;
            drink.isDisplay = false;
            this.gameObject.transform.position = counterLocation.position;
            isDragging = !isDragging;
            shaker.canShake = !shaker.canShake;
        }

        if (onButton)
        {
            // PLACEHOLDER
        }

        if (onCustomer)
        {
            // PLACEHOLDER
        }
	}
    
    void FixedUpdate()
    {
        if (canDrag)
        {   
            if (isDragging)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(pos.x, pos.y, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check tag
        if (collision.gameObject.tag == "Button")
        {
            onButton = true;
        }
        
        if (collision.gameObject.tag == "Counter")
        {
            onCounter = true;
        }

        if (collision.gameObject.tag == "Customer")
        {
            onCustomer = true;
        }

        if (collision.gameObject.tag == "Trash")
        {
            onTrash = true;
        }
    }   

    void OnTriggerExit2D(Collider2D collision)
    {
        // Check tag
          if (collision.gameObject.tag == "Button")
        {
            onButton = false;
        }
        
        if (collision.gameObject.tag == "Counter")
        {
            onCounter = false;
        }

        if (collision.gameObject.tag == "Customer")
        {
            onCustomer = false;
        }

        if (collision.gameObject.tag == "Trash")
        {
            onTrash = false;
        }
    }

    void OnDestroy()
    {
        DraggableDrink.Pool.Remove(this);
        CupSpawner.numDrinks--;
    }
}
