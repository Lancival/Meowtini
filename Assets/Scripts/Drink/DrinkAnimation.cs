using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkAnimation : MonoBehaviour
{   
    [SerializeField] private LiquidSprites liquidSprites;
    [SerializeField] private Animator animLiquid;
    [SerializeField] private Drink drink; // Reference to drink
    [SerializeField] private Shaker shaker; // Reference to shaker;
    [SerializeField] private SpriteRenderer liquidSpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get array to liquid sprites;
        liquidSprites = FindObjectOfType<LiquidSprites>();
        shaker = FindObjectOfType<Shaker>();
        drink = GetComponent<Drink>();
        if (liquidSpriteRenderer == null)
        {
            Debug.LogError("Don't forget to assign liquid sprite renderer!");
        }   
    }

    public void FillCup()
    {
        string object_name = this.name;
        
        // Expect the first character of the name to be 0, 1, 2
        char prefix = object_name[0];
        string color = shaker.GetMajorityColor();

        switch (prefix)
        {
            case '0':
                if (color == "Orange")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('0', "Orange");
                if (color == "Purple")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('0', "Purple");
                if (color == "Clear")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('0', "Clear");
                if (color == "Blue")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('0', "Blue");
                break;
            case '1':
                if (color == "Orange")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('1', "Orange");
                if (color == "Purple")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('1', "Purple");
                if (color == "Clear")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('1', "Clear");
                if (color == "Blue")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('1', "Blue");
                break;
            case '2':
                if (color == "Orange")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('2', "Orange");
                if (color == "Purple")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('2', "Purple");
                if (color == "Clear")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('2', "Clear");
                if (color == "Blue")
                    liquidSpriteRenderer.sprite = liquidSprites.GetSprite('2', "Blue");
                break;
        }

        animLiquid.SetTrigger("Fill");

    }
}
