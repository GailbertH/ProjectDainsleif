using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public const string ATTACK = "PlayerAttackAnim";

	[SerializeField] float baseAttackCharge = 3;
	[SerializeField] Animation characterAnim;
	[SerializeField] Animation playerAnim;
	private PlayerCharactersData characterData;
	private float attackCharge = 3;
	private float life = 50;
	private float baseHealth = 50;
	[SerializeField] private int id;
	private int ID {set { id = value;} get{ return id;}}
	public bool IsAlive
	{
		get{ return life > 0; }
	}
	void Awake()
	{
		attackCharge = baseAttackCharge;
	}
	public void CheckAttack()
	{
		if (!IsAlive)
			return;
		
		attackCharge--;
		if (attackCharge <= 0)
			this.Attack ();
	}

	public void Attack()
	{
		attackCharge = baseAttackCharge;
		this.PlayerAttackAnim ();
		GameManager.Instance.PlayerAttack ();
	}

	public void DecreseLife(int decVal)
	{
		life -= decVal;
		GameManager.Instance.GameUI.UpdatePlayerIconHealth (ID, life/baseHealth);
		PlayDamgedAnim ();
	}

	public void PlayDamgedAnim()
	{
		if (playerAnim != null)
		{
			if (playerAnim.isPlaying)
				playerAnim.Stop ();
			playerAnim.Play ();
		}
	}

	private void PlayerAttackAnim()
	{
		if (characterAnim.IsPlaying (ATTACK))
			characterAnim.Stop (ATTACK);

		characterAnim.Play (ATTACK);
	}
}
