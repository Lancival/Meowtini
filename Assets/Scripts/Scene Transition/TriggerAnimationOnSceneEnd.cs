using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class TriggerAnimationOnSceneEnd : MonoBehaviour
{

	private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
    	if (SceneLoader.instance != null)
    		SceneLoader.instance.onSceneEnd.AddListener(Trigger);
    }

    private void Trigger(float duration)
    {
    	animator.SetTrigger("Trigger");
    }
}
