using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite cherry;
    [SerializeField] Sprite cream;
    [SerializeField] Sprite ice;
    [SerializeField] Sprite olive;
    [SerializeField] Sprite strawberry;
    [SerializeField] Sprite lime;
    [SerializeField] Sprite mint; 

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        sprites.Add("cherry", cherry);
        sprites.Add("cream", cream);
        sprites.Add("ice", ice);
        sprites.Add("olive", olive);
        sprites.Add("strawberry", strawberry);
        sprites.Add("lime", lime);
        sprites.Add("mint", mint);
    }

    public Sprite GetSprite(string keyword)
    {
        return sprites[keyword];
    }
}
