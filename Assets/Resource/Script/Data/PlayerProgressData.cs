using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerProgressData 
{
	public long StageLevel;
	public long KillCount;
	public DateTime StartTime;
	public long PlayDuration;
	public long GoldCount;
	public int WaveCount;


	public void Initialize()
	{
		StageLevel = 0;
		KillCount = 0;
		StartTime = DateTime.Now;
		PlayDuration = 0;
		GoldCount = 0;
		WaveCount = 0;
	}
	public void Save()
	{
	}
	public void Load()
	{
	}
}
