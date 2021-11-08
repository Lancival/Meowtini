using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderDrink : MonoBehaviour
{
    [Header("Drink Components")]
    [SerializeField] Image cup;
    [SerializeField] Image liquid;
    [SerializeField] Image topping1;
    [SerializeField] Image topping2;
    [SerializeField] Image coldness;


    [SerializeField] Drink drinkBlueprint;
    [SerializeField] SpriteList sprites;

    
    void Start()
    {
        Invoke("AssembleDrink", 0.2f);
    }

    public void AssembleDrink()
    {
        // Choose right cup & pour base liquid
        switch (drinkBlueprint.cupShape)
        {
            case Drink.CupShapes.funnel:
                cup.sprite = sprites.GetSprite("funnel");
                liquid.sprite = sprites.GetSprite("l_funnel");
                break;
            case Drink.CupShapes.cylinder:
                cup.sprite = sprites.GetSprite("cylinder");
                liquid.sprite = sprites.GetSprite("l_cylinder");
                break;
            case Drink.CupShapes.flask:
                cup.sprite = sprites.GetSprite("flask");
                liquid.sprite = sprites.GetSprite("l_flask");
                break;
        }
        // TODO: apply correct color masks
        // Handle toppings
        int numIce = 0;
        bool topping1Finished = false; // to determine when to modify topping2
        foreach (string topping in drinkBlueprint.toppings.Keys)
        {
            if (topping == "ice")
            {
                numIce = drinkBlueprint.toppings[topping];
            } else
            {
                if (topping1Finished)
                {
                    topping2.sprite = sprites.GetSprite(topping);
                    break;
                } else
                {
                    topping1.sprite = sprites.GetSprite(topping);
                    topping1Finished = true;
                }
            }
        }
        switch (numIce)
        {
            case 1:
                coldness.sprite = sprites.GetSprite("cold1");
                break;
            case 2:
                coldness.sprite = sprites.GetSprite("cold2");
                break;
            case 3:
                coldness.sprite = sprites.GetSprite("cold3");
                break;
            default:
                coldness.enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
