using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] int startingTimer = 60; // In seconds
    public int counter;
    public int timer;
    private float startTime;

    [SerializeField] SpriteRenderer timerIcon;
    private Color iconColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        timer = startingTimer;
        startTime = Time.time;
        timerIcon.color = iconColor;
        InvokeRepeating("HandleTimer", 0, 1);
    }

    // Executes 50 times per second
    private void FixedUpdate()
    {
        //counter++;
        //if (counter >= 50)
        //{
        //    timer--;
        //    counter = 0;
        //}
        // HandleTimer();
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
            iconColor = Color.Lerp(Color.green, Color.yellow, 0.05f);  // Probably a better way to do it
        }
        else if (timer == startingTimer / 3 * 2) // Set to yellow
        {
            iconColor = Color.yellow;
        }
        else if (timer > startingTimer / 3)  // Transition from yellow to red
        {
            iconColor = new Vector4(iconColor.r, iconColor.g - 0.05f, iconColor.b, iconColor.a);
        } 
        else if (timer == startingTimer / 3)  // Set to red
        {
            iconColor = Color.red;
        } 
        else
        {
            // iconColor = new Vector4(iconColor.r + 255 / 20, iconColor.g, iconColor.b, iconColor.a);
        }
        timerIcon.color = iconColor;
        if (timer == 0)
        {
            Debug.Log("ORDER FAILED");
            timerIcon.color = Color.black;
        }
    }
}
