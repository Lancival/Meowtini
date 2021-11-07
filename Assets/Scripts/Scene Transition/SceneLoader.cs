using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	// Current instance of SceneLoader, if one exists
	public static SceneLoader instance {get; private set;}

	// Whether this SceneLoader has started loading the next scene
	private bool loading = false;
	
    [Header("Settings")]
	   [Tooltip("Name of the next scene.")]
	   [SerializeField] private string nextScene;

	   [Tooltip("Duration of the scene transition.")]
	   [Range(0, 60)]
	   [SerializeField] private float duration = 1.0f;

    [Header("Events")]
	   [Tooltip("Event that triggers end-of-scene transitions.")]
	   public UnityEvent<float> onSceneEnd = new UnityEvent<float>();

	void Awake()
	{
		// Initialize reference at the start of scene
		instance = this;
	}

	// Loads the nextScene scene asynchronously, waiting at least duration seconds
	private static IEnumerator LoadSceneAsync(string nextScene, float duration)
    {
        if (nextScene == null)
        {
            Debug.LogError("No name provided for next scene.");
            yield break;
        }
        else if (duration < 0)
        {
        	Debug.LogError("Duration of LoadSceneAsync must be non-negative.");
        	yield break;
        }

        // Time at which the scene loading started.
        float time = Time.time;

        // Start loading the next scene asynchronously.
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(nextScene);
        if (sceneLoad == null)
        {
            yield break;
        }

        // Wait until the next scene has been loaded in the background.
        sceneLoad.allowSceneActivation = false;
        while (!sceneLoad.isDone)
        {
            // Don't allow the next scene to activate until the transition duration has finished.
            if (Time.time - time > duration)
                sceneLoad.allowSceneActivation = true;

            yield return null;
        }
    }

    #region Wrapper Functions
    // Load the next scene
    public void LoadNextScene() {
    	if (nextScene == null)
    	{
    		Debug.Log("Error: Next scene name not provided in SceneLoader script.");
    	}
    	else
    	{
    		LoadNextScene(nextScene);
    	}
    }

    // Load the scene named sceneName
    public void LoadNextScene(string sceneName)
    {
        if (sceneName == null)
        {
            Debug.LogError("Next scene name was null in SceneLoader script.");
        }
        else if (!loading)
        {
            loading = true;
            onSceneEnd.Invoke(duration);
            StartCoroutine(LoadSceneAsync(sceneName, duration));
        }
    }
    #endregion
}
