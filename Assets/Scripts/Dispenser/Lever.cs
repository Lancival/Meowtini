using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Lever : MonoBehaviour
{
    [SerializeField] private Dispenser dispenser;
    [SerializeField] private float timer;
    [SerializeField] private float period = 0.5f;
    [SerializeField] private bool startTimer = false;

    void Start()
    {
        timer = period;
    }

    void OnMouseDown()
    {
    	startTimer = true;
    }

    void OnMouseDrag()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                dispenser.DispenseLiquid();
                timer = period;
            }
        }
    }

    void OnMouseUp()
    {
        startTimer = false;
        timer = period;
    }
}
