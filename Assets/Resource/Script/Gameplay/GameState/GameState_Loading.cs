using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dainsleif.Game
{
	public class GameState_Loading : GameState_Base<GameState>  
	{
		private Coroutine cdNextState;

		public GameState_Loading (GameManager manager) : base (GameState.LOADING, manager)
		{
		}

		public override void GoToNextState()
		{
			Manager.StateMachine.SwitchState (GameState.INGAME);
		}

		public override bool AllowTransition (GameState nextState)
		{
			return (nextState == GameState.INGAME);
		}

		public override void Start ()
		{
			//Loading hahahaha
			cdNextState = Manager.StartCoroutine (DelayedStateSwitch (2f));
		}

		public override void End () 
		{
			if (cdNextState != null && Manager != null)
				Manager.StopCoroutine (cdNextState);

			cdNextState = null;
		}

		public override void Destroy ()
		{
			End ();
			base.Destroy ();
		}


		private IEnumerator DelayedStateSwitch (float delay)
		{
			yield return new WaitForSeconds(delay);
			//SoundManager.Instance.PlayBGM ();
			//Manager.LoadingScreenPlay ();
			GoToNextState ();
		}
	}
}
