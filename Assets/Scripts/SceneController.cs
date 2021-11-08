using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] Canvas workstation;
    [SerializeField] Canvas counter;
    [SerializeField] SpriteRenderer background;

    [SerializeField] SpriteList sprites;


    // Start is called before the first frame update
    void Start()
    {
        workstation.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (workstation.enabled)
        {
            background.sprite = sprites.GetSprite("workstation");
        } else
        {
            background.sprite = sprites.GetSprite("counter");
        }
    }

    

}
