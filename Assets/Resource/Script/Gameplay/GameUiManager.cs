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
				if(Input.touchCount < i)
					return;
									
				Input.GetTouch (i);
				Vector3 pos = new Vector3 (Input.GetTouch (i).position.x, Input.GetTouch (i).position.y, 0);
				LogHandler.AddLog ("Button Touch " + Camera.main.ScreenToWorldPoint(pos).ToString());
			}
		}
		if(GameManager.Instance != null)
			GameManager.Instance.PlayerAttack ();
	}
}
