using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    /// <summary>
    /// Drink creation steps: choose cup shape, pour alcohol, choose liquid, add ice, shake, pour liquid, add toppings
    /// </summary>


    public Dictionary<string, float> liquids;  // String is the liquid name, float is the amount
    public Dictionary<string, int> toppings;  // String is topping name, int is number of said topping


    public float capacity; // Maximum volume of liquid drink can hold
    public float curVolume = 0; // Current volume of the drink

    Dictionary<string, SpriteRenderer> toppingSprites;
    Dictionary<string, SpriteRenderer> liquidSprites;

    // All drinks active in the scene
    public static HashSet<Drink> Pool = new HashSet<Drink>();

    [Header("Components")]
    [SerializeField] SpriteRenderer cream;
    [SerializeField] SpriteRenderer cherry;
    [SerializeField] SpriteRenderer ice0;
    [SerializeField] SpriteRenderer ice1;
    [SerializeField] SpriteRenderer ice2;
    [SerializeField] SpriteRenderer strawberry;
    [SerializeField] SpriteRenderer mint;
    [SerializeField] SpriteRenderer olive;
    [SerializeField] SpriteRenderer lime;

    [Header("Liquid Masks")]
    [SerializeField] SpriteRenderer purple;
    [SerializeField] SpriteRenderer orange;
    [SerializeField] SpriteRenderer clear;
    [SerializeField] SpriteRenderer blue;

    public enum CupShapes
    {
        funnel,
        cylinder,
        flask
    }
    public CupShapes cupShape;


    void Awake()
    {
        toppingSprites = new Dictionary<string, SpriteRenderer>();
        toppingSprites.Clear();
        liquidSprites = new Dictionary<string, SpriteRenderer>();
        liquidSprites.Clear();
        SetSprites();
        liquids = new Dictionary<string, float>();
        liquids.Clear();
        toppings = new Dictionary<string, int>();
        toppings.Clear();
    }

    // Gets called everytime the drink is activated
    private void OnEnable()
    {
        Drink.Pool.Add(this);
    }

    // Gets called everytime the drink is deactivated
    private void OnDisable()
    {
        Drink.Pool.Remove(this);
    }
    
    private void FixedUpdate()
    {
        DisplayDrink();
    }
    
    void SetSprites()
    {
        toppingSprites.Add("cream", cream);
        toppingSprites.Add("cherry", cherry);
        toppingSprites.Add("ice0", ice0);
        toppingSprites.Add("ice1", ice1);
        toppingSprites.Add("ice2", ice2);
        toppingSprites.Add("strawberry", strawberry);
        toppingSprites.Add("mint", mint);
        toppingSprites.Add("olive", olive);
        toppingSprites.Add("lime", lime);
        foreach (string elem in toppingSprites.Keys)
        {
            toppingSprites[elem].enabled = false;
        }
        liquidSprites.Add("purple", purple);
        liquidSprites.Add("orange", orange);
        liquidSprites.Add("clear", clear);
        liquidSprites.Add("blue", blue);
        foreach(string elem in liquidSprites.Keys)
        {
            liquidSprites[elem].enabled = false;
        }
    }

    public static Drink FindClosestDrink(Vector3 pos)
    {
        Drink result = null;
        float dist = float.PositiveInfinity;
        var e = Drink.Pool.GetEnumerator();
        while(e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude;
            if(d < dist)
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }

    public void SetCupShape(CupShapes shape)
    {
        cupShape = shape;
    }

    public void SetCupShape(int shape)
    {
        cupShape = (CupShapes) shape; 
    }

    public void AddTopping(string topping)
    {
        if (toppings.ContainsKey(topping))
        {
            toppings[topping]++;
        }
        else
        {
            toppings[topping] = 1;
        }
    }

    public void PrintInfo()
    {
        Debug.Log("Cup shape: " + cupShape);
        foreach(string topping in toppings.Keys)
        {
            Debug.Log(topping);
        }
        foreach(string liquid in liquids.Keys)
        {
            Debug.Log(liquid);
        }
    }
    public void DisplayDrink()
    {
        // Liquid
        foreach(string liquid in liquids.Keys)
        {
            liquidSprites[liquid].enabled = true;
        }
        // Toppings
        foreach(string topping in toppings.Keys)
        {
            if (topping == "ice")
            {
                // Yay I love brute force >:(
                switch (toppings[topping])
                {
                    case 1:
                        toppingSprites["ice0"].enabled = true;
                        break;
                    case 2:
                        toppingSprites["ice0"].enabled = true;
                        toppingSprites["ice1"].enabled = true;
                        break;
                    case 3:
                        toppingSprites["ice0"].enabled = true;
                        toppingSprites["ice1"].enabled = true;
                        toppingSprites["ice2"].enabled = true;
                        break;
                }
            } else
            {
                toppingSprites[topping].enabled = true;
            }
        }
    }
}
