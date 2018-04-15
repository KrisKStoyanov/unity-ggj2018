using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedrunTimer : MonoBehaviour {

	public static SpeedrunTimer timer;

	public bool speedrunning;

	public Canvas speedrunCanvas;
	public Text speedrunTimeText;

	public GameObject endTimePanel;
	public Text currentTimeText;
	public Text bestTimeText;

	public float achievedTime;

	void Awake () {
		timer = this;
		achievedTime = 0;
		speedrunning = true;
		StartCoroutine (TrackSpeedrun ());
		/*if (timer == null) {
			timer = this;
			DontDestroyOnLoad (gameObject);
		} else if (timer != this) {
			Destroy (gameObject);
		}
		speedrunning = false;
		speedrunCanvas.gameObject.SetActive (false);*/
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.V)){
			speedrunCanvas.gameObject.SetActive (true);
			//speedrunning = true;
		}
	}

	public IEnumerator TrackSpeedrun (){
		while (true) {
			if (!speedrunning) {

				if (PersistentDataManager.pdm.bestTime != 0) {
					if (achievedTime < PersistentDataManager.pdm.bestTime) {
						PersistentDataManager.pdm.bestTime = achievedTime;
						PersistentDataManager.pdm.Save ();
					}
				} else {
					PersistentDataManager.pdm.bestTime = achievedTime;
					PersistentDataManager.pdm.Save ();
				}

				endTimePanel.SetActive (true);
				bestTimeText.text = "Best Time: " + PersistentDataManager.pdm.bestTime;
				currentTimeText.text = "Current Time: " + achievedTime;
				StopAllCoroutines ();
			} 
			else {
				achievedTime += 1f * Time.deltaTime;
				achievedTime = (Mathf.Round (achievedTime * 100f) / 100f);
				if ((Mathf.Round (Time.time * 100f) / 100f) < 10) {
					speedrunTimeText.text = "0" + achievedTime;
				} else {
					speedrunTimeText.text = "" + achievedTime;
				}
			}
			yield return null;
		}
	}
}