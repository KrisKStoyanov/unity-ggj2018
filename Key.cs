using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
	public int keyNum;
	public GameObject soundTracker;
	public AudioClip clip;
	public string colour;

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<KeyInventory> () != null) 
		{
			soundTracker.transform.position = other.gameObject.transform.position;
			soundTracker.gameObject.GetComponent<AudioSource>().Play();
			other.GetComponent<KeyInventory> ().keylist.Add (keyNum);

			TextAlert.alert.StartCoroutine (TextAlert.alert.PlayAlert ("Picked up " + colour + " identification disk"));

			this.gameObject.SetActive (false);
		}
	}
}
