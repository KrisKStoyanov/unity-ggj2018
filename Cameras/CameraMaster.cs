using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour
{
	public bool isDestroyed;

    public delegate void GeneralEventManager();
    public event GeneralEventManager EventCameraSwitchTo;
    public event GeneralEventManager EventCameraSwitchFrom;
    public event GeneralEventManager EventCameraDestroy;

    public void CallEventCameraSwitchTo()
    {
        if(EventCameraSwitchTo != null)
        {
            EventCameraSwitchTo();
        }
    }

    public void CallEventCameraSwitchFrom()
    {
        if (EventCameraSwitchFrom != null)
        {
            EventCameraSwitchFrom();
        }
    }

    public void CallEventCameraDestroy()
    {
        if (EventCameraDestroy != null)
        {
            EventCameraDestroy();
        }
    }
}
