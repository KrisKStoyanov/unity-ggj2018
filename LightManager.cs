 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	public MeshRenderer[] lightMeshes;

	public float flickerWait;
	public float flickerReset;  
	public float speedOfLightLoss;
	public float rateOfLightLoss;

	public Light[] flickeringLights;
	//public Material lightOnMat;
	public Material lightOffMat;

	public float[] defaultLightIntensity;

	private Material[] rendererMats;

	public Material[] defaultmats;

	public int[] indexes;

	public float[] trackedIntensity;

	// Update is called once per frame
	void Start(){
		indexes = new int[lightMeshes.Length];
		defaultLightIntensity = new float[lightMeshes.Length];
		rendererMats = new Material[lightMeshes.Length];
		defaultmats = new Material[lightMeshes.Length];
		flickeringLights = new Light[lightMeshes.Length];
		trackedIntensity = new float[lightMeshes.Length];

		flickerReset = flickerWait;

		for (int i = 0; i < lightMeshes.Length; i++) {
			for (int y = 0; y < lightMeshes[i].materials.Length; y++) {
				if (lightMeshes [i].materials [y].GetColor("_EmissionColor") != Color.black)
				{
					defaultmats [i] = lightMeshes [i].materials [y];
					//rendererMats [i] = lightMeshes [i].materials [y];
					indexes [i] = y;
				}
			}
		}

		for (int i = 0; i < flickeringLights.Length; i++) {
			flickeringLights [i] = lightMeshes [i].gameObject.GetComponentInChildren<Light> ();
			if (flickeringLights [i] != null) {
				defaultLightIntensity [i] = flickeringLights [i].intensity;
				trackedIntensity[i] = flickeringLights[i].intensity;
			} else {
				defaultLightIntensity [i] = 3;
				trackedIntensity [i] = 3;
			}
		}

		StartCoroutine (BeginFlickering ());
	}

	private IEnumerator BeginFlickering (){
		while (true)
		{
			for (int i = 0; i < trackedIntensity.Length; i++)
			{
				//Material[] rendererMats = lightMeshes[i].materials;
				rendererMats = lightMeshes[i].materials;

				trackedIntensity[i] -= rateOfLightLoss * speedOfLightLoss;
				if (flickeringLights [i] != null) {
					flickeringLights [i].intensity = trackedIntensity[i];
				}

				if (trackedIntensity[i] <= 0) {
					rendererMats [indexes [i]] = lightOffMat;
					flickerWait -= Time.deltaTime;
				}

				if (flickerWait <= 0) {
					flickerWait = flickerReset;
					rendererMats[indexes[i]] = defaultmats[i];
					trackedIntensity [i] = defaultLightIntensity [i];
				}

				lightMeshes [i].materials = rendererMats;
			}
				
			/*foreach(Light flickeringLight in flickeringLights){
				flickeringLight.intensity -= rateOfLightLoss * speedOfLightLoss;
				//flickeringLight.range = flickeringLight.intensity * 2;
				if(flickeringLight.intensity <= 0){
					flickeringLight.intensity = defaultLightIntensity;
			}*/
			yield return null;
		}

	}
}
