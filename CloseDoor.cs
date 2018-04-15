using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour {

	public string myText;

	private Animator myAnimator;

	void OnEnable()
	{
		if (transform.parent.GetComponent<Animator> () != null) 
		{
			myAnimator = transform.parent.GetComponent<Animator> ();
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Drone> () != null) 
		{
			if (myAnimator != null) 
			{
				myAnimator.SetBool ("Activate", false);					
			}
			TextAlert.alert.StartCoroutine (TextAlert.alert.PlayAlert (myText));
			gameObject.SetActive (false);
		}
	}
}
