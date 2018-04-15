using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour 
{
	public AudioSource muhAudioSource;
	public AudioClip[] clips;
	public Cinemachine.CinemachineVirtualCamera finalCamera;
	public CameraGroup camGroup;
	public GameObject finalScene;

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Drone> () != null)
		{
			StartCoroutine (EndGameCoroutine(other.GetComponent<Drone>()));
		}
	}

	IEnumerator EndGameCoroutine(Drone muhDrone)
	{
		if (SpeedrunTimer.timer.speedrunning) {
			SpeedrunTimer.timer.speedrunning = false;
		}

		muhDrone.transform.GetChild (0).parent = null;
		muhDrone.gameObject.SetActive (false);
		muhAudioSource.clip = clips [0];
		muhAudioSource.Play ();
		yield return new WaitForSeconds (3f);
		muhAudioSource.clip = clips [1];
		muhAudioSource.Play ();
		camGroup.ChangeUsableCameras (muhDrone);
		yield return new WaitForSeconds (2f);
		finalScene.SetActive (true);
		yield return new WaitForSeconds (1f);
		finalScene.transform.GetChild (0).gameObject.SetActive (true);
		yield return new WaitForSeconds (3f);

		CameraController.instance.holder.gameObject.SetActive (false);
		WhiteNoise.instance.FlashStatic ();
		CameraController.instance.victoryScreen.SetActive(true);
	}

}
