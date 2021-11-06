using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Lever : MonoBehaviour
{
    [SerializeField] private Dispenser dispenser;

    void OnMouseDown()
    {
    	dispenser.DispenseLiquid();
    }
}
