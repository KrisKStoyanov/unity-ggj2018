using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTrigger : MonoBehaviour 
{
	public int keyNeeded;

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
		if (other.GetComponent<KeyInventory> () != null) 
		{
			foreach (int key in other.GetComponent<KeyInventory>().keylist) 
			{
				if (key == keyNeeded) 
				{
					if (myAnimator != null) 
					{
						myAnimator.SetBool ("Activate", true);					
					}
					return;
				}
			}
			StartCoroutine (TextAlert.alert.PlayAlert (myText));
		}
	}
}
