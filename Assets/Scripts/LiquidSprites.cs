using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSprites : MonoBehaviour
{
   public Sprite[] liquidSprites;

   public Sprite GetSprite(char prefix, string name)
   {
      Sprite retSprite = liquidSprites[0];
      foreach(var sprite in liquidSprites)
      {
         if (sprite.name.Contains(name) && sprite.name[0] == prefix)
            retSprite = sprite;
      }
      return retSprite;
   }
}
