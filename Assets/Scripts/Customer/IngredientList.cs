using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientList : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Display Elements")]
    [SerializeField] SpriteRenderer icon;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] SpriteList spriteList;

    void Start()
    {
        
    }

    public void DisplayIngredient(string ingred, int amount)
    {
        // Display the icon
        Sprite display = spriteList.GetSprite(ingred);
        icon.sprite = display;
        icon.enabled = true;
        // Display text
        text.SetText(" = "+ amount);
    }

}
