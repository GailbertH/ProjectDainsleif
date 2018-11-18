using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconUI : MonoBehaviour {

	[SerializeField] Image healthBar;
	[SerializeField] Image ultiBar;
	[SerializeField] Image characterIcon;
	[SerializeField] Button characterButton;

	public void UpdateHealthBar(float fillValue)
	{
		healthBar.fillAmount = fillValue;
		this.CheckCharacterButton ();
	}
	public void UpdateUlttiBar(float fillValue)
	{
		ultiBar.fillAmount = fillValue;
		this.CheckCharacterButton ();
	}
	private void CheckCharacterButton()
	{
		characterButton.interactable = (ultiBar.fillAmount >= 1f && healthBar.fillAmount > 0f);
	}
}
