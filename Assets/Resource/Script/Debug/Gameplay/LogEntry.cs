using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogEntry : MonoBehaviour 
{
	[SerializeField] private Text textComp;
	public string text
	{
		get
		{
			return textComp.text;
		}
		set
		{
			textComp.text = value;
		}
	}
}
