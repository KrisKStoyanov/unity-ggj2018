using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchStrengthManager : MonoBehaviour
{
	public static GlitchStrengthManager instance;
	public GlitchEffect effect;

	[Range (0, 1)]
	public float Strength = 0.0f;

	public float MinIntensity = 1.5f;
	public float MaxIntensity = 10;

	public float MinWait = 0.2f;
	public float MaxWait = 1.5f;

	void Awake ()
	{
		instance = this;
	}

	IEnumerator Start ()
	{
		while (true)
		{
			Strength = Mathf.Clamp01 (Strength);
			if (Strength < 0.1f)
			{
				effect.enabled = false;
			}
			else
			{
				effect.intensity = Mathf.Pow (Random.Range (
					MinIntensity, MaxIntensity), 2) * (Strength * 3.0f);
				effect.enabled = true;
			}

			yield return new WaitForSeconds (Random.Range (MinWait, MaxWait) * (Strength));
		}
	}
}