using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class DialoguePlayer : MonoBehaviour
{
	public static DialoguePlayer instance;

	Queue<AudioClip> clipQueue = new Queue<AudioClip>();
	private AudioSource source;

	void Awake ()
	{
		instance = this;
		source = GetComponent<AudioSource> ();
	}

	public void PlayAudio (AudioClip clip)
	{
		clipQueue.Enqueue (clip);

		PlayAudioInternal ();
	}

	private void PlayAudioInternal ()
	{
		if (source.isPlaying)
		{
			Invoke ("PlayAudioInternal", source.clip.length - source.time);
		}
		else
		{
			if (clipQueue.Count == 0)
				return;
			
			AudioClip clip = clipQueue.Dequeue ();
			source.clip = clip;
			source.Play ();

			Invoke ("PlayAudioInternal", source.clip.length + 0.025f);
		}
	}
}