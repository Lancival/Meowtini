using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Topping : MonoBehaviour
{

	private Camera cam;
	private Drink target;
    private string name = "DefaultTopping";

	void Awake()
	{
		cam = Camera.main;
		target = null;
	}

    void OnMouseDrag()
    {
    	Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
    	transform.position = new Vector3(pos.x, pos.y, 0);
    }

    void OnMouseUp()
    {
        if (target != null)
        {
    	   transform.position = target.transform.position;
           target.AddTopping(name);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
    	Drink drink = collider.gameObject.GetComponent<Drink>();
    	if (drink != null)
    	{
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
