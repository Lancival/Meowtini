using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] int startingTimer = 60; // In seconds
    public int counter;
    public int timer;

    [SerializeField] SpriteRenderer timerIcon;
    private Color iconColor = Color.green;


    // Start is called before the first frame update
    void Start()
    {
        timer = startingTimer;
        timerIcon.color = iconColor;
        InvokeRepeating("HandleTimer", 0, 1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Updates the game accordingly 
    void HandleTimer()
    {
        timer--;
        if (timer > startingTimer / 3 * 2) // Transition from green to yellow
        {
            // iconColor = new Vector4(iconColor.r + 0.05f, iconColor.g, iconColor.b, iconColor.a);
            iconColor = Color.Lerp(Color.yellow, Color.green, 0.05f * (timer - startingTimer / 3 * 2));  // Probably a better way to do it
        }
        else if (timer == startingTimer / 3 * 2) // Set to yellow
        {
            iconColor = Color.yellow;
        }
        else if (timer > startingTimer / 3)  // Transition from yellow to red
        {
            iconColor = Color.Lerp(Color.red, Color.yellow, 0.05f * (timer - startingTimer / 3));
        } 
        else if (timer == startingTimer / 3)  // Set to red
        {
            iconColor = Color.red;
        } 
        else  // Transition from red to (nearly) black
        {
            iconColor = Color.Lerp(Color.Lerp(Color.black, Color.red, 0.2f) , Color.red, 0.05f * (timer));
        }
        timerIcon.color = iconColor;
        if (timer == 0)
        {
            Debug.Log("ORDER FAILED");
            timerIcon.color = Color.black;
        }
    }
}
