using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour 
{
	//To be change this should be spawned and handle by Game Manager not a singleton.
	private static GameUiManager instance;
	public static GameUiManager Instance { get { return instance; } }

	[SerializeField] Text killCountText;
	[SerializeField] Text waveCountText;
	[SerializeField] List<PlayerIconUI> playerIcons;
	void Awake()
	{
		instance = this;
	}

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
		if (GameManager.Instance != null) 
		{
			GameManager.Instance.MainPlayerAttack ();
			GameManager.Instance.PlayerAttack ();
		}
	}

	public void UpdateUI()
	{
		if(killCountText != null)
			killCountText.text = string.Format("Kills: {0}", GameManager.Instance.PlayerData().KillCount);
		if(waveCountText != null)
			waveCountText.text = string.Format("Waves: {0}", GameManager.Instance.PlayerData().WaveCount);
	}

	public void UpdatePlayerIconHealth(int indexOfIcon, float fillAmount)
	{
		if (indexOfIcon > playerIcons.Count)
			return;

		playerIcons [indexOfIcon].UpdateHealthBar (fillAmount);
	}

	public void UpdatePlayerIconUlti(int indexOfIcon, float fillAmount)
	{
		if (indexOfIcon > playerIcons.Count)
			return;

		playerIcons [indexOfIcon].UpdateUlttiBar (fillAmount);
	}
}
