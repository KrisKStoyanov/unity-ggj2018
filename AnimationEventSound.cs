using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSound : MonoBehaviour {

	public AudioClip clip;

	public void PlaySound()
	{
		AudioSource.PlayClipAtPoint (clip, transform.position, 1f);
	}
}
