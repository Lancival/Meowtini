using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
	[Tooltip("DropLocation where Draggable objects can go.")]
    [SerializeField] private DropLocation dropLocation;

    //[Tooltip("Levers to dispense liquid.")]
    //[SerializeField] private Lever[] levers;

    public void DispenseLiquid()
    {
    	Debug.Log("Liquid dispensing functionality not yet implemented.");
    }
}
