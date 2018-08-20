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

	void Update()
	{
		if (stateMachine != null) 
		{
			stateMachine.Update ();
		}
	}
	#endregion

	public void ExitGame()
	{
		if (StateMachine.GetCurrentState.State == GameState.INGAME) 
		{
			StateMachine.SwitchState (GameState.EXIT);
		}
	}
}
