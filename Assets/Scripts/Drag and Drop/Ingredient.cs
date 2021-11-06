using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Ingredient : MonoBehaviour
{

	private Camera cam;
	private Vector3? targetPosition;

	void Awake()
	{
		cam = Camera.main;
		targetPosition = null;
	}

    void OnMouseDrag()
    {
    	Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
    	transform.position = new Vector3(pos.x, pos.y, 0);
    }

    void OnMouseUp()
    {
    	if (targetPosition == null)
    	{
    		Destroy(gameObject);
    		return;
    	}
    	transform.position = (Vector3) targetPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
    	Draggable draggable = collider.gameObject.GetComponent<Draggable>();
    	if (draggable != null)
    	{
    		targetPosition = draggable.transform.position;
    		OnDraggableEnter(draggable);
    	}
    }

    void OnTriggerExit2D(Collider2D collider)
    {
    	Draggable draggable = collider.gameObject.GetComponent<Draggable>();
    	if (draggable != null)
    	{
    		targetPosition = null;
    		OnDraggableExit(draggable);
    	}
    }

    protected virtual void OnDraggableEnter(Draggable draggable)
    {
    	return;
    }

    protected virtual void OnDraggableExit(Draggable draggable)
    {
    	return;
    }
}
