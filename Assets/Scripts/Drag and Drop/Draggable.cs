using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Draggable : MonoBehaviour
{

	private Vector3 initialPosition;
	private Vector3? targetPosition;

	private Camera cam;

	void Awake()
	{
		initialPosition = transform.position;
		targetPosition = null;

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
    	transform.position = (targetPosition ?? initialPosition);
    }

    public void SetTarget(Vector3 target)
    {
    	targetPosition = target;
    }

    public void RemoveTarget()
    {
    	targetPosition = null;
    }
}
