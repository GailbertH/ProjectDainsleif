using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHandler : MonoBehaviour {
	private static LogHandler instance = null;
	public static LogHandler Instance
	{
		get
		{
			return instance;
		}
	}

	private List<LogEntry> messages;
	[SerializeField] private int maxMessages = 30;

	[SerializeField] private GameObject sample;
	[SerializeField] private Transform logParent;

	void Awake()
	{
		instance = this;
		messages = new List<LogEntry>();

		DontDestroyOnLoad(this.gameObject);
	}

	void OnDestroy()
	{
		instance = null;
	}

	public static void AddLog(string log)
	{
		if(instance != null)
		{
			instance.LogIt(log);
		}
	}

	public void LogIt(string log)
	{
		if(messages.Count > 0 && messages.Count >= maxMessages)
		{
			Destroy(messages[0]);
			messages.RemoveAt(0);
		}

		GameObject entry = Instantiate(sample);
		entry.name = "Log";
		LogEntry screenLog = entry.GetComponent<LogEntry>();
		screenLog.text = log;
		entry.transform.SetParent(logParent, false);
		entry.SetActive(true);

		messages.Add(screenLog);
	}

	public void Clear()
	{
		if(messages.Count > 0)
		{
			for(int last = messages.Count - 1; last >= 0; last--)
			{
				Destroy(messages[last].gameObject);
			}

			messages.Clear();
		}
	}
}
