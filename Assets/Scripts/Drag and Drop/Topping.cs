using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/*
Valid topping names:
   cream;
   cherry;
   ice1;
   ice2;
   strawberry;
   mint;
   olive;
   lime; 
*/

public class Topping : MonoBehaviour
{
	// Requires a spawner
	public ToppingSpawner spawner;

	private Camera cam;
	private Drink target;
    [SerializeField] private string toppingName = "DefaultTopping";

	void Awake()
	{
		cam = Camera.main;
		target = null;
	}

	// When the topping is first clicked, we'll spawn a duplicate behind it.
	void OnMouseDown() {
		Debug.Log("Clicked on topping...Spawning a new one");
		spawner.SpawnTopping();
	}

    void OnMouseDrag()
    {
    	Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
    	transform.position = new Vector3(pos.x, pos.y, 0);
    }

    void OnMouseUp()
    {
		// All toppings should be attached to drink, only ice is separate
        if (target != null && !toppingName.Contains("ice"))
        {
    	   transform.position = target.transform.position;
           target.AddTopping(toppingName);
        }
		else
		{
			// Dealing with ice, get reference to shaker--there should only be one
			GameObject obj = GameObject.FindWithTag("Shaker");
			Shaker shaker = obj.GetComponent<Shaker>();
			if (shaker.canDropIce)
			{
				if (shaker.numIceCubes < 3)
				{
					shaker.numIceCubes++;
				}
			}
		}
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
    	Drink drink = collider.gameObject.GetComponent<Drink>();
    	if (drink != null)
    	{
			Debug.Log("Entered trigger radius of drink");
    		target = drink;
    	}
    }

    void OnTriggerExit2D(Collider2D collider)
    {
    	Drink drink = collider.gameObject.GetComponent<Drink>();
    	if (drink != null)
    	{
    		target = null;
    	}
    }
}
