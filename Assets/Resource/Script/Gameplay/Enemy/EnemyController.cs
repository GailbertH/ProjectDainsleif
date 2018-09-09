using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public const string SPAWN = "EnemySpawn";

	public int life = 20;
	[SerializeField] Animation enemyAnim;
	[SerializeField] Animation characterAnim;

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

}
