using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dainsleif.Game;

public class GameManager : MonoBehaviour 
{
	private static GameManager instance;
	private GameStateMachine stateMachine;

	public static GameManager Instance { get { return instance; } }
	public GameStateMachine StateMachine { get { return this.stateMachine; } }
	public GameUiManager GameUI { get { return GameUiManager.Instance; } }
	private Coroutine updateRoutine;
	[SerializeField] private float gameSpeed = 1;
	private bool isReviving = false;
	private PlayerProgressData playerData = new PlayerProgressData();
	#region Monobehavior Function

	[SerializeField] private List<EnemyController> enemyController;
	[SerializeField] private List<PlayerController> playerController;
	private List<EnemyController> deadEnemy;
	private List<PlayerController> deadPlayer;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		stateMachine = new GameStateMachine (this);
		deadEnemy = new List<EnemyController> ();
		playerData.Initialize ();
		if(GameUI != null)
			GameUI.UpdateUI ();
	}

	void OnDestroy()
	{
		if (updateRoutine != null)
		{
			StopCoroutine (updateRoutine);
		}
		if (stateMachine != null)
		{
			stateMachine.Destroy ();
			stateMachine = null;
		}
	}

	void FixedUpdate()
	{
		/*
		if (stateMachine != null) 
		{
			stateMachine.Update ();
		}
		*/
	}
	#endregion
	public PlayerProgressData PlayerData()
	{
		return playerData;
	}
	public float GameSpeed
	{
		set{ gameSpeed = value; }
		get{ return gameSpeed; }
	}
	public void BeginStateUpdate()
	{
		updateRoutine = StartCoroutine (TimedUpdate());
	}

	private IEnumerator TimedUpdate()
	{
		yield return new WaitForEndOfFrame ();
		while(stateMachine != null)
		{
			yield return new WaitForSeconds (GameSpeed);
			stateMachine.Update ();
		}
	}

	public void AIAttack()
	{
		if (PlayerManager.Instance != null)
			PlayerManager.Instance.PlayerAIAttack ();
		else
			Debug.LogError ("PlayerManager is null!");

		if (EnemyManager.Instance != null)
			EnemyManager.Instance.EnemyAIAttack ();
		else
			Debug.LogError ("EnemyManage is null!");
	}

	public void ExitGame()
	{
		if (StateMachine.GetCurrentState.State == GameState.INGAME) 
		{
			StateMachine.SwitchState (GameState.EXIT);
		}
	}
		
	public void MainPlayerAttack()
	{
		if (PlayerManager.Instance != null)
			PlayerManager.Instance.MainCharacterAttack ();
	}

	public void PlayerAttack()
	{
		if (enemyController == null || enemyController.Count <= 0)
			return;

		bool hasChanges = false;
		int rand = Random.Range (0, enemyController.Count);
		enemyController [rand].DecreseLife (1);
		//LogHandler.AddLog ("Life Dec " + rand + " Remaining Life =" +  enemyController [rand].life);
		if (enemyController [rand].life <= 0) 
		{
			enemyController [rand].gameObject.SetActive (false);
			deadEnemy.Add (enemyController [rand]);
			enemyController.RemoveAt (rand);
			CheckWinOrLoseCondition ();
			playerData.KillCount++;
			hasChanges = true;
		}
			
		if (enemyController.Count <= 0 && isReviving == false) 
		{
			playerData.WaveCount++;
			isReviving = true;
			Invoke ("DelayReviveEnemy", 0.5f);
			hasChanges = true;
		}

		if(hasChanges && GameManager.instance != null)
			GameUiManager.Instance.UpdateUI ();
	}

	public void EnemyAttack()
	{
		if (playerController == null || playerController.Count <= 0)
			return;

		int rand = Random.Range (0, playerController.Count);
		playerController [rand].DecreseLife (1);
	}

	private void DelayReviveEnemy()
	{
		Debug.Log ("ALL IS DEAD, REBIRTH INITIALIZE");
		enemyController = new List<EnemyController> ();
		enemyController = deadEnemy;
		deadEnemy = new List<EnemyController> ();
		for (int i = 0; i < enemyController.Count; i++) 
		{
			enemyController [i].life = 20;
			enemyController [i].gameObject.SetActive (true);
			enemyController [i].PlaySpawnAnim ();
		}
		isReviving = false;
	}

	private void CheckWinOrLoseCondition()
	{
		if (enemyController.Count <= 0)
			LogHandler.AddLog ("WINNER!!!!");
	}
}
