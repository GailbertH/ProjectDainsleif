using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
	private static EnemyManager instance;
	public static EnemyManager Instance { get { return instance; } }
	public List<EnemyController> enemies;

	void Awake()
	{
		instance = this;
	}

	public void EnemyAIAttack()
	{
		if (enemies == null)
			return;

		for (int i = 0; enemies.Count > i; i++) 
		{
			enemies [i].CheckAttack ();
		}
	}
}
