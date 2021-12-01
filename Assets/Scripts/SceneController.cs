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

    private bool inWorkstation;

    // Start is called before the first frame update
    void Start()
    {
        workstation.enabled = false;
        inWorkstation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (workstation.enabled)
        {
            inWorkstation = true;
            background.sprite = sprites.GetSprite("workstation");
        } else
        {
            inWorkstation = false;
            background.sprite = sprites.GetSprite("counter");
        }
    }

    // Checks if current scene is workstation
    public bool isInWorkstation()
    {
        return inWorkstation;
    }

}
