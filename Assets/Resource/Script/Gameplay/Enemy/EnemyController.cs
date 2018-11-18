using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public const string SPAWN = "EnemySpawn2";
	public const string ATTACK = "EnemyAttack";

	public int life = 20;
	[SerializeField] Animation enemyAnim;
	[SerializeField] Animation characterAnim;
	[SerializeField] float baseAttackCharge = 3;
	private float attackCharge = 3;
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
		GameManager.Instance.EnemyAttack ();
		PlayAttackAnim ();
	}

	public void DecreseLife(int decVal)
	{
		life -= decVal;
		if (enemyAnim != null) {
			if (enemyAnim.isPlaying)
				enemyAnim.Stop ();
			enemyAnim.Play ();
		}
	}

	public void PlaySpawnAnim()
	{
		if (characterAnim != null)
			characterAnim.Play (SPAWN);
	}

	public void PlayAttackAnim()
	{
		if (characterAnim != null)
			characterAnim.Play (ATTACK);
	}
}
