using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dainsleif.Game
{
	public enum GameState
	{
		LOADING = 0,
		INGAME = 1,
		RESULT = 2,
		EXIT = 3
	}
	public class GameState_Base<GameState> 
	{

		private GameState state;
		private GameManager manager;

		public GameState State { get { return state; } }
		public GameManager Manager { get { return manager; } }

		public GameState_Base(GameState state, GameManager manager)
		{
			this.state = state;
			this.manager = manager;
		}

		public virtual bool AllowTransition (GameState nextState)
		{
			return true;
		}

		public virtual void GoToNextState() {}
		public virtual void Start () 
		{
			Debug.Log (this.state.ToString ());
		}
		public virtual void Update () {}
		public virtual void End () {}
		public virtual void Destroy () 
		{
			End ();
			manager = null;
		}
	}
}
