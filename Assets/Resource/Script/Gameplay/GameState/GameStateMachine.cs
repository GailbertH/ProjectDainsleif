using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dainsleif.Game
{
	public class GameStateMachine  
	{
		public delegate void OnStateSwitch(GameState nextState);
		public event OnStateSwitch OnStatePreSwitchEvent = null;

		private Dictionary<GameState, GameState_Base<GameState>> states = new Dictionary<GameState, GameState_Base<GameState>>();
		private GameState_Base<GameState> currentState = null;
		private List<GameState> prevGameState;

		public GameStateMachine (GameManager manager)
		{
			states = new Dictionary<GameState, GameState_Base<GameState>> ();

			GameState_Loading loading = new GameState_Loading (manager);
			GameState_InGame inGame = new GameState_InGame (manager);
			GameState_Exit exit = new GameState_Exit (manager);

			states.Add (loading.State, (GameState_Base<GameState>)loading);
			states.Add (inGame.State, (GameState_Base<GameState>)inGame);
			states.Add (exit.State, (GameState_Base<GameState>)exit);

			currentState = loading;
			currentState.Start ();

			prevGameState = new List<GameState> ();
			prevGameState.Add (currentState.State);

			manager.BeginStateUpdate ();
		}

		public void Update ()
		{
			if (currentState != null)
				currentState.Update ();				
		}

		public void Destroy ()
		{
			if (states != null)
			{
				foreach (GameState key in states.Keys)
				{
					states [key].Destroy ();
				}
				states.Clear ();
				states = null;
			}
		}

		public GameState_Base<GameState> GetCurrentState
		{
			get { return currentState; }
		}

		public string GetPreviousStateList ()
		{
			string prevStates = "PREVIOUS STATES: ";

			#if UNITY_EDITOR
			if(prevGameState != null)
			{
				for(int i = prevGameState.Count-1; i >= 0; i--)
				{
					prevStates += "\n-> " + prevGameState[i].ToString();
				}
			}
			#endif

			return prevStates;
		}


		public bool SwitchState (GameState newState)
		{
			Debug.Log("SWITCH STATE " + currentState.State.ToString() + " to " + newState.ToString());
			bool switchSuccess = false;
			if (states != null && states.ContainsKey (newState))
			{
				if (currentState == null)
				{
					currentState = states [newState];
					currentState.Start ();
					switchSuccess = true;
				}
				else if (currentState.AllowTransition (newState))
				{
					currentState.End ();
					currentState = states [newState];
					currentState.Start ();
					switchSuccess = true;
				}
				else
				{
					Debug.Log (string.Format ("{0} does not allow transition to {1}", currentState.State, newState));
				}
			}

			if (switchSuccess)
			{
				// Updating state history
				#if UNITY_EDITOR
				if(prevGameState != null)
				{
					prevGameState.Add(newState);
					if(prevGameState.Count > 20)
					{
						prevGameState.RemoveAt(0);
					}
				}
				#endif

				if (this.OnStatePreSwitchEvent != null)
				{
					this.OnStatePreSwitchEvent (newState);
				}
			}
			else
			{
				Debug.Log ("States dictionary not ready for switching to " + newState);
			}

			return switchSuccess;
		}
	}
}
