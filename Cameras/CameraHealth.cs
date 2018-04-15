using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHealth : MonoBehaviour
{
	private CameraMaster cameraMaster;
	private CameraGroup myCameraGroup;

	public ParticleSystem smoke;

	public float health = 0;

	public bool isActive = false;

	public float healthRate = 0.1f;

	void OnEnable()
	{
		SetInitialReferences ();
	}

	void SetInitialReferences()
	{
		if (GetComponent<CameraMaster>() != null) 
		{
			cameraMaster = GetComponent<CameraMaster> ();
		}

		if (transform.parent.parent.GetComponent<CameraGroup> () != null) 
		{
			myCameraGroup = transform.parent.parent.GetComponent<CameraGroup> ();
		}
	}

	void Update()
	{
		if (isActive) 
		{
			health += healthRate * Time.deltaTime;

			if (health >= 1f) 
			{
				DestroyCam ();
				DeactivateHealth ();
			}
		}
	}

	void DeactivateHealth()
	{
		isActive = false;
	}

	void DestroyCam()
	{
		cameraMaster.isDestroyed = true;
		myCameraGroup.availableCameras--;
		myCameraGroup.CheckAvailability();
		if (smoke != null) 
		{
			smoke.Play ();
		}

		WhiteNoise.instance.FlashStatic ();

		CameraController.instance.switchSound.Play ();

		CameraController.instance.offlineScreen.SetActive 
			(CameraController.instance.curCamera.GetComponent<CameraMaster> ().isDestroyed);


		//CameraController.instance.availableCameras.Remove (transform);

		//CameraController.instance.NextCamera ();
	}
}
