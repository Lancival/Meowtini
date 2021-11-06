using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DropLocation : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
    	Draggable draggable = collider.gameObject.GetComponent<Draggable>();
    	if (draggable != null)
    	{
    		draggable.SetTarget(transform.position);
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

    protected virtual void OnDraggableEnter(Draggable draggable)
    {
    	return;
    }

    protected virtual void OnDraggableExit(Draggable draggable)
    {
    	return;
    }
}
