using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

	void Update ()
	{
		if (Input.anyKeyDown)
		{
			//Load best time
			PersistentDataManager.pdm.Load();
			SceneManager.LoadScene (1);
		}
	}
}