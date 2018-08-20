using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dainsleif.Game
{
	public class GameState_InGame : GameState_Base<GameState>   
	{

		public GameState_InGame (GameManager manager) : base (GameState.INGAME, manager)
		{
		}

		public override void GoToNextState()
		{
			Manager.StateMachine.SwitchState (GameState.EXIT);
		}

		public override bool AllowTransition (GameState nextState)
		{
			return (nextState == GameState.EXIT);
		}
		public override void Start () {}
		public override void Update () {}
		public override void End () {}
	}
}
