using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
	private static PlayerManager instance;
	public static PlayerManager Instance { get { return instance; } }
	public PlayerController mainCharacter;
	public List<PlayerController> players;

	void Awake()
	{
		instance = this;
	}

	public void MainCharacterAttack()
	{
		mainCharacter.Attack ();
	}

	public void PlayerAIAttack()
	{
		for (int i = 0; i < players.Count; i++)
			players [i].CheckAttack ();
	}
}
