using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public const string ATTACK = "PlayerAttackAnim";

	[SerializeField] float baseAttackCharge = 3;
	[SerializeField] Animation characterAnim;

	private float attackCharge = 3;

	void Awake()
	{
		attackCharge = baseAttackCharge;
	}
	public void CheckAttack()
	{
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

	private void PlayerAttackAnim()
	{
		if (characterAnim.IsPlaying (ATTACK))
			characterAnim.Stop (ATTACK);

		characterAnim.Play (ATTACK);
	}
}
