using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public static CameraController instance;

	public GameObject offlineScreen;
	public GameObject gameoverScreen;
	public GameObject victoryScreen;

	[Header ("State")]
	public Transform curCamera;
	public List<Transform> availableCameras = new List<Transform>();
	private int camIndex = 0;

	public CameraUI[] UIControllers;

	[Header ("Switch")]
	public float switchRate = 1f;
	private float nextSwitch;

	[Header ("UI")]
	public CameraUI CameraUIPrefab;
	public Transform holder;

	[Header ("Audio")]
	public AudioSource hackingSource;

	public AudioSource switchSound;

	void Awake ()
	{
		instance = this;
	}

	void Start()
	{
		SetInitialReferences ();
	}

	void SetInitialReferences()
	{
		nextSwitch = Time.time;
		curCamera = availableCameras [0];
		if (GetComponent <AudioSource> () != null) 
		{
			switchSound = GetComponent<AudioSource> ();
		}
		Invoke ("InitialiseFirstCamera", 0.05f);
	}

	void InitialiseFirstCamera()
	{
		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchTo ();
	}

	void Update()
	{
		GlitchStrengthManager.instance.Strength = curCamera.GetComponent<CameraHealth> ().health;



		for (int i = UIControllers.Length - 1; i >= 0; i--)
		{
			CameraUI ui = UIControllers [i];

			ui.UpdateHackMeter (availableCameras [i].GetComponent<CameraHealth> ().health);

			if (availableCameras [i] == curCamera)
				ui.MakeLarge ();
			else
				ui.MakeSmall ();
		}


		if (Time.time > nextSwitch)
		{
			if (Input.GetKeyDown (KeyCode.E)) 
			{
				NextCamera ();
				nextSwitch = Time.time + switchRate;
			}
			if (Input.GetKeyDown (KeyCode.Q)) 
			{
				PreviousCamera ();
				nextSwitch = Time.time + switchRate;
			}
		}
	}

	public void NextCamera()
	{
		if (availableCameras.Count <= 0) 
		{
			GameOver ();
		}
		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchFrom ();

		if (camIndex + 1 >= availableCameras.Count) 
		{
			camIndex = 0;
		} 
		else 
		{
			camIndex++;
		}
		curCamera = availableCameras [camIndex];

		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchTo ();

		PlaySwitchSound ();
	}

	void PreviousCamera()
	{
		if (availableCameras.Count <= 0) 
		{
			GameOver ();
		}

		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchFrom ();

		if (camIndex - 1 < 0) 
		{
			camIndex = availableCameras.Count-1;
		} 
		else 
		{
			camIndex--;
		}
		curCamera = availableCameras [camIndex];

		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchTo ();

		PlaySwitchSound ();
	}

	public void UpdateAvailableCameras(List<Transform> newCams)
	{
		availableCameras = newCams;
		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchFrom ();

	
		curCamera = availableCameras [0];
		curCamera.GetComponent<CameraMaster> ().CallEventCameraSwitchTo ();
		camIndex = 0;


		PlaySwitchSound ();

		nextSwitch = Time.time + switchRate;


		foreach (Transform child in holder)
		{
			Destroy (child.gameObject);
		}

		UIControllers = new CameraUI[availableCameras.Count];
		for (int i = 0; i < availableCameras.Count; i++)
		{
			CameraUI ui = Instantiate (CameraUIPrefab, holder);

			ui.transform.localScale = Vector3.one;
			ui.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;

			if (i == 0)
				ui.MakeLarge ();
			else
				ui.MakeSmall ();

			UIControllers [i] = ui;
		}
	}

	void PlaySwitchSound()
	{
		WhiteNoise.instance.FlashStatic ();

		offlineScreen.SetActive (curCamera.GetComponent<CameraMaster> ().isDestroyed);

		if (switchSound != null &&
		    switchSound.clip != null) 
		{
			switchSound.Play ();
		}

		if (!curCamera.GetComponent<CameraMaster> ().isDestroyed)
		{
			//hackingSource.pitch = curCamera.GetComponent<CameraHealth>().healthRate / 30.0f;
			hackingSource.Play ();
			hackingSource.time = curCamera.GetComponent<CameraHealth> ().health * 30.0f;
			StartCoroutine (FadeInHacking ());
		}
		else
		{
		}
	}

	IEnumerator FadeInHacking ()
	{
		float time = 0.0f;
		float volume = 1.0f;
		float duration = 1.5f;

		while (time > duration) {
			hackingSource.volume = (time / duration) * time;

			time += Time.deltaTime;
			yield return null;
		}

		hackingSource.volume = volume;
	}

	public void GameOver()
	{
		print ("GameOver");

		gameoverScreen.SetActive (true);

		holder.gameObject.SetActive (false);
	}
}