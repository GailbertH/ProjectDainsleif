using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour 
{
	public void ButtPress()
	{
		Vector3 input = Input.mousePosition;
		LogHandler.AddLog ("ButtonPress " + Camera.main.ScreenToWorldPoint(input).ToString());
		if (Input.touchCount > 0)
		{
			for (int i = 0; i < Input.touchCount; i++) 
			{
				Input.GetTouch (i);
				LogHandler.AddLog ("Button Touch " + Input.GetTouch (i).position.ToString());
			}
		}
	}
}
