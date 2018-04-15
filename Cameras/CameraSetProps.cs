using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetProps : MonoBehaviour 
{
	private CameraMaster cameraMaster;
	private CameraHealth cameraHealth;
	private Cinemachine.CinemachineVirtualCamera cam;
	private GameObject camModel;

	void OnEnable()
	{
		SetInitialReferences ();
		cameraMaster.EventCameraSwitchTo += ActivateCamera;
		cameraMaster.EventCameraSwitchFrom += DeactivateCamera;
	}

	void OnDisable()
	{
		cameraMaster.EventCameraSwitchTo -= ActivateCamera;
		cameraMaster.EventCameraSwitchFrom -= DeactivateCamera;
	}

	void SetInitialReferences()
	{
		if (GetComponent<CameraHealth> () != null) {
			cameraHealth = GetComponent<CameraHealth> ();
		}

		if (GetComponent<CameraMaster> () != null) {
			cameraMaster = GetComponent<CameraMaster> ();
		}

		if (GetComponent<Cinemachine.CinemachineVirtualCamera> () != null) {
			cam = GetComponent<Cinemachine.CinemachineVirtualCamera> ();
			cam.enabled = false;
		}


		camModel = transform.parent.GetChild (0).gameObject;
	}

	void ActivateCamera()
	{
		if(cameraHealth != null)
		{
			cameraHealth.isActive = true;
		}

		if (cam != null) 
		{
			cam.enabled = true;
		}

		if (camModel != null) 
		{
			camModel.SetActive (false);
		}
	}

	void DeactivateCamera()
	{
		if(cameraHealth != null)
		{
			cameraHealth.isActive = false;
		}

		if (cam != null) 
		{
			cam.enabled = false;
		}

		if (camModel != null) 
		{
			camModel.SetActive (true);
		}
	}
}
