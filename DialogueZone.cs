using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]
public class DialogueZone : MonoBehaviour
{
	public static List<AudioClip> PlayedAudio = new List<AudioClip> ();

	public AudioClip[] Clips;

	public void OnTriggerEnter (Collider other)
	{
		Drone drone = other.gameObject.GetComponent<Drone> ();

		if (drone != null)
		{
			PlayAudioClips (Clips);
			Destroy (gameObject);
		}
	}

	static void PlayAudioClips (AudioClip[] clips)
	{
		int checkIndex = Random.Range (0, clips.Length - 1);

		if (!PlayedAudio.Contains (clips [checkIndex]))
		{
			PlayAudioClip (clips [checkIndex]);
			return;
		}

		checkIndex = Random.Range (0, clips.Length - 1);

		if (!PlayedAudio.Contains (clips [checkIndex]))
		{
			PlayAudioClip (clips [checkIndex]);
			return;
		}

		foreach (AudioClip clip in clips)
		{
			if (!PlayedAudio.Contains (clip))
			{
				PlayAudioClip (clip);
				return;
			}
		}

		PlayedAudio = new List<AudioClip> ();

		PlayAudioClips (clips);
	}

	static void PlayAudioClip (AudioClip clip)
	{
		PlayedAudio.Add (clip);
		DialoguePlayer.instance.PlayAudio (clip);
	}
}