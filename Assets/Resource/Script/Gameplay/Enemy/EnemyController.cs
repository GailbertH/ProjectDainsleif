using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public int life = 20;
	public void DecreseLife(int decVal)
	{
		life -= decVal;
	}

}
