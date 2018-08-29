using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dainsleif.Game;

public class GameManager : MonoBehaviour 
{
	private static GameManager instance;
	private GameStateMachine stateMachine;

	public static GameManager Instance { get { return instance; } }
	public GameStateMachine StateMachine{ get { return this.stateMachine; } }

	#region Monobehavior Function
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		stateMachine = new GameStateMachine (this);
	}

	void OnDestroy()
	{
		if (stateMachine != null)
		{
			stateMachine.Destroy ();
			stateMachine = null;
		}
	}

	void FixedUpdate()
	{
		if (stateMachine != null) 
		{
			stateMachine.Update ();
		}
	}
	#endregion

	public void AIAttack()
	{
		Debug.Log ("ATTACK");
	}

	public void ExitGame()
	{
		if (StateMachine.GetCurrentState.State == GameState.INGAME) 
		{
			StateMachine.SwitchState (GameState.EXIT);
		}
	}



	[SerializeField] private List<EnemyController> enemyController;

	public void PlayerAttack()
	{
		if (enemyController.Count <= 0)
			return;

		int rand = Random.Range (0, enemyController.Count);
		enemyController [rand].DecreseLife (1);
		LogHandler.AddLog ("Life Dec " + rand + " Remaining Life =" +  enemyController [rand].life);
		if (enemyController [rand].life <= 0) 
		{
			enemyController [rand].gameObject.SetActive (false);
			enemyController.RemoveAt (rand);
			CheckWinOrLoseCondition ();
		}
	}

	private void CheckWinOrLoseCondition()
	{
		if (enemyController.Count <= 0)
			LogHandler.AddLog ("WINNER!!!!");
	}
}
