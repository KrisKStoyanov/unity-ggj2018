using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroup : MonoBehaviour
{
	public Transform[] GroupCameras;

	private CameraController cameraController;

	public int availableCameras;


	void OnEnable()
	{
		SetInitialReferences ();
	}

	void SetInitialReferences()
	{
		if (transform.parent.GetComponent<CameraController> () != null) 
		{
			cameraController = transform.parent.GetComponent<CameraController> ();
		}

		availableCameras = GroupCameras.Length;
	}

	public void CheckAvailability()
	{
		if (availableCameras <= 0) 
		{
			if (cameraController != null) 
			{
				cameraController.GameOver ();
			}
		}
	}


	public void OnTriggerEnter (Collider other)
	{
		Drone drone = other.gameObject.GetComponent<Drone> ();

		ChangeUsableCameras (drone);
	}

	public void ChangeUsableCameras(Drone drone)
	{
		if (drone != null)
		{
			CameraMaster[] masters = transform.GetComponentsInChildren <CameraMaster> ();

			List<Transform> transforms = new List<Transform> (masters.Length);

			foreach (CameraMaster master in masters) {
				transforms.Add (master.transform);
			}

			CameraController.instance.UpdateAvailableCameras (transforms);
		}
	}
}