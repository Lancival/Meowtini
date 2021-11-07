using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] Canvas workstation;
    [SerializeField] Canvas counter;


    // Start is called before the first frame update
    void Start()
    {
        workstation.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
