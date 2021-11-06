using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Draggable : MonoBehaviour
{

	private Vector3 initialPosition;

    // Previous DropLocation occupied by this Draggable, may be null
    private DropLocation previous;
    // DropLocation to move this drink to, may be null
	private DropLocation target;

	private Camera cam;

	void Awake()
	{
		initialPosition = transform.position;
        previous = null;
		target = null;

		cam = Camera.main;
	}

    void OnMouseDown()
    {
    	initialPosition = transform.position;
    }

    void OnMouseDrag()
    {
    	Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
    	transform.position = new Vector3(pos.x, pos.y, 0);
    }

    void OnMouseUp()
    {
        if (target)
        {
            // Remove Draggable from previous DropLocation
            previous?.RemoveDraggable(this);
            previous = target;

            // Add Draggable to new DropLocation
            transform.position = target.transform.position;
            target.AddDraggable(this);
        }
        else
        {
            // Reset location
            transform.position = initialPosition;
        }
    }

    public void SetTarget(DropLocation location)
    {
    	target = location;
    }

    public void RemoveTarget()
    {
    	target = null;
    }
}
