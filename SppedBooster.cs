using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SppedBooster : MonoBehaviour {

	public Vector3 boostForce;
	public GameObject playerDrone;
	public AudioSource myAudioSource;

	void OnEnable()
	{
		myAudioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			playerDrone = other.gameObject;
			StartCoroutine (BoostPlayer ());
			myAudioSource.Play ();
		}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Player") {
			StopAllCoroutines ();
			this.gameObject.SetActive (false);
		}
	}

	private IEnumerator BoostPlayer (){
		while (true) {

			playerDrone.gameObject.GetComponent<Drone> ().Velocity = boostForce;

			yield return null;
		}
	}

}
