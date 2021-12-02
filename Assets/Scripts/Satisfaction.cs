using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Satisfaction : MonoBehaviour
{
    [SerializeField] RectTransform bar;
    [SerializeField] Image barColor;
    [SerializeField] Slider slider;  // Entirely temporary. Used to test the bar
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    [SerializeField] SpriteList sprites;

    float maxSatisfaction = 100;
    float satisfaction;

    Sprite starLit;
    Sprite starUnlit;
    // Start is called before the first frame update
    void Start()
    {
        satisfaction = 100;
        
        slider.minValue = 0;
        slider.maxValue = 100;
        starLit = sprites.GetSprite("starLit");
        starUnlit = sprites.GetSprite("starUnlit");
    }

    // Update is called once per frame
    void Update()
    {
        satisfaction = GameManager.Instance.GetSatisfaction();
        UpdateStars();
        bar.localScale = new Vector3(Mathf.Lerp(bar.localScale.x, satisfaction / maxSatisfaction, 0.4f), 1);
    }

    void UpdateStars()
    {
        if (satisfaction > 40)
        {
            star1.sprite = starLit;
        } else
        {
            star1.sprite = starUnlit;
        }
        if (satisfaction > 70)
        {
            star2.sprite = starLit;
        } else
        {
            star2.sprite = starUnlit;
        }
        if (satisfaction > 95)
        {
            star3.sprite = starLit;
        } else
        {
            star3.sprite = starUnlit;
        }
    }
}
