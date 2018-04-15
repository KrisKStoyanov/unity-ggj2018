using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/WhiteNoise")]
public class WhiteNoise : ImageEffectBase
{
	public static WhiteNoise instance;

	public float TimeRemaining = 0.0f;
	public float PingDuration = 0.5f;

	void Awake ()
	{
		instance = this;
	}

	public void FlashStatic ()
	{
		TimeRemaining = PingDuration;
	}

	void Update ()
	{
		TimeRemaining -= Time.deltaTime;
	}

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (TimeRemaining > 0.0f)
			Graphics.Blit (source, destination, material);
		else
			Graphics.Blit (source, destination);
	}
}
