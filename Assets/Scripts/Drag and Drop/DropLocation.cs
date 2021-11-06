using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DropLocation : MonoBehaviour
{	 
    public bool isOccupied {get; private set;}

    void OnTriggerEnter2D(Collider2D collider)
    {
    	Draggable draggable = collider.gameObject.GetComponent<Draggable>();
    	if (draggable != null)
    	{
    		draggable.SetTarget(this);
    		OnDraggableEnter(draggable);
    	}
    }

    void OnTriggerExit2D(Collider2D collider)
    {
    	Draggable draggable = collider.gameObject.GetComponent<Draggable>();
    	if (draggable != null)
    	{
    		draggable.RemoveTarget();
    		OnDraggableExit(draggable);
    	}
    }

    public virtual void AddDraggable(Draggable draggable)
    {
        isOccupied = true;
    }

    public virtual void RemoveDraggable(Draggable draggable)
    {
        isOccupied = false;
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
