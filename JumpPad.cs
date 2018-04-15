using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

	public float jumpForce = 5;

	public GameObject playerDrone;

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			playerDrone = other.gameObject;
			StartCoroutine (Levitate ());
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			StopAllCoroutines ();

		}
	}

	private IEnumerator Levitate (){
		while (true) {

			playerDrone.GetComponent<Drone> ().Velocity = new Vector3 (0, jumpForce, 0);
			yield return null;
		}
	}

}
