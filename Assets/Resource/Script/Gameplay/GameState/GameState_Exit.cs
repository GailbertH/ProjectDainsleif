using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dainsleif.Game
{
	public class GameState_Exit : GameState_Base<GameState>    
	{

		public GameState_Exit (GameManager manager) : base (GameState.EXIT, manager)
		{
		}

		public override void Start ()
		{ 
			//START UNLOADING
			LoadingManager.Instance.SetSceneToUnload (SceneNames.GAME_UI + "," + SceneNames.GAME_SCENE);
			LoadingManager.Instance.SetSceneToLoad (SceneNames.LOBBY_SCENE);
			LoadingManager.Instance.LoadGameScene ();
		}
		public override void End () 
		{ 
			//END EVERYTHING
		}
	}
}
