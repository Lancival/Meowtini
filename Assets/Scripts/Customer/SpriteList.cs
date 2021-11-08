using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    [Header("Ingredients")]
    [SerializeField] Sprite cherry;
    [SerializeField] Sprite cream;
    [SerializeField] Sprite ice;
    [SerializeField] Sprite mint;
    [SerializeField] Sprite olive;
    [SerializeField] Sprite strawberry;
    [SerializeField] Sprite lime;

    [Header("Cups")]
    [SerializeField] Sprite cylinder;
    [SerializeField] Sprite funnel;
    [SerializeField] Sprite flask;

    [Header("Coldness")]
    [SerializeField] Sprite cold1;
    [SerializeField] Sprite cold2;
    [SerializeField] Sprite cold3;

    [Header("Liquids")]
    [SerializeField] Sprite l_cylinder;
    [SerializeField] Sprite l_funnel;
    [SerializeField] Sprite l_flask;
    [SerializeField] Sprite blue;
    [SerializeField] Sprite purple;
    [SerializeField] Sprite orange;
    [SerializeField] Sprite clear;

    [Header("Scene")]
    [SerializeField] Sprite workstation;
    [SerializeField] Sprite counter;
    [SerializeField] Sprite starLit;
    [SerializeField] Sprite starUnlit; 

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        sprites.Add("cherry", cherry);
        sprites.Add("cream", cream);
        sprites.Add("ice", ice);
        sprites.Add("mint", mint);
        sprites.Add("olive", olive);
        sprites.Add("strawberry", strawberry);
        sprites.Add("lime", lime);
        sprites.Add("cylinder", cylinder);
        sprites.Add("funnel", funnel);
        sprites.Add("flask", flask);
        sprites.Add("cold1", cold1);
        sprites.Add("cold2", cold2);
        sprites.Add("cold3", cold3);
        sprites.Add("l_cylinder", l_cylinder);
        sprites.Add("l_funnel", l_funnel);
        sprites.Add("l_flask", l_flask);
        sprites.Add("blue", blue);
        sprites.Add("orange", orange);
        sprites.Add("purple", purple);
        sprites.Add("clear", clear);
        sprites.Add("workstation", workstation);
        sprites.Add("counter", counter);
        sprites.Add("starLit", starLit);
        sprites.Add("starUnlit", starUnlit);
    }

    public Sprite GetSprite(string keyword)
    {
        return sprites[keyword];
    }
}
