using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FadeAudioOnSceneTransition : MonoBehaviour
{

	[SerializeField] private AudioMixer mixer;
	[SerializeField] private float duration = 1.0f;
	private const string groupName = "Master Volume";

	void Start()
	{
		StartCoroutine(AudioUtility.FadeMixerGroup(mixer, groupName, duration, 1));
		SceneLoader.instance.onSceneEnd.AddListener(FadeOut);
	}

	private void FadeOut(float d)
	{
		StartCoroutine(AudioUtility.FadeMixerGroup(mixer, groupName, d, 0));
	}
}