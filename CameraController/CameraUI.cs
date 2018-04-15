using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUI : MonoBehaviour
{
	public Image HackMeter;
	public Image CameraIcon;

	public Vector2 SmallSize;
	public Vector2 LargeSize;

	public Color Unhacked;
	public Color Hacked;

	public void MakeSmall ()
	{
		GetComponent <RectTransform> ().sizeDelta = SmallSize;

		gameObject.GetComponent <LayoutElement> ().preferredWidth = SmallSize.x;
		gameObject.GetComponent <LayoutElement> ().preferredHeight = SmallSize.y;
	}

	public void MakeLarge ()
	{
		GetComponent <RectTransform> ().sizeDelta = LargeSize;

		gameObject.GetComponent <LayoutElement> ().preferredWidth = LargeSize.x;
		gameObject.GetComponent <LayoutElement> ().preferredHeight = LargeSize.y;
	}

	public void UpdateHackMeter (float value)
	{
		HackMeter.fillAmount = value;
		CameraIcon.color = Color.Lerp (Unhacked, Hacked, value);
	}
}