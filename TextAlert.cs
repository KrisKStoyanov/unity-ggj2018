using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlert : MonoBehaviour 
{
	public static TextAlert alert;

	[SerializeField]
	private CanvasGroup myCanvasGroup; 

	[SerializeField]
	private Text myText;

	void Awake()
	{
		alert = this;
	}


	public IEnumerator PlayAlert(string text)
	{
		if(text != "")
		{
			myText.text = text;
		}

		float time = 0.0f;

		while (time < 0.2f) 
		{
			time += Time.deltaTime;
			myCanvasGroup.alpha = time / 0.2f;
			yield return null;
		}

		myCanvasGroup.alpha = 1f;

		yield return new WaitForSeconds (2f);

		time = 0.0f;

		while (time < 0.2f) 
		{
			time += Time.deltaTime;
			myCanvasGroup.alpha = 1.0f - (time / 0.2f);
			yield return null;
		}

		myCanvasGroup.alpha = 0f;
	}
}
