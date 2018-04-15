using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class PersistentDataManager : MonoBehaviour {

	public static PersistentDataManager pdm;

	public float bestTime;

	void Awake (){
		if (pdm == null) {
			pdm = this;
			DontDestroyOnLoad (gameObject);
		} else if (pdm != this) {
			Destroy (gameObject);
		}
	}

	public void Save(){
		GameData saveData = new GameData ();
		string saveStateString = JsonUtility.ToJson (saveData, true);
		SHA256Managed crypt = new SHA256Managed ();
		string hash = string.Empty;
		byte[] crypto = crypt.ComputeHash (Encoding.UTF8.GetBytes (saveStateString), 0, Encoding.UTF8.GetByteCount (saveStateString));

		foreach (byte bit in crypto) {
			hash += bit.ToString ("x2");
		}

		saveData.bestSavedTime = bestTime;
		saveData.hashOfContents = hash;

		string saveStatePath = Path.Combine (Application.persistentDataPath, "speedrunTime.json");
		File.WriteAllText (saveStatePath, JsonUtility.ToJson (saveData, true));
	}

	public void Load(){
		string savePath = Path.Combine(Application.persistentDataPath, "speedrunTime.json");
		if (File.Exists (savePath)) {
			string saveFile = File.ReadAllText (savePath);
			GameData loadedData = JsonUtility.FromJson<GameData> (saveFile);

			bestTime = loadedData.bestSavedTime;
		}
	}

}

[Serializable]
public class GameData {
	public float bestSavedTime;
	public string hashOfContents;
}
