using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour {

	public static TimeManagerScript timeManagerScriptInstance;
	
	public float currentTime;

	public delegate void TimerFinished();
	public TimerFinished OnTimerFinished;

	public delegate void TimerUpdate();
	public TimerUpdate OnTimerUpdated ;

	private float timerDuration,startTime;
	private bool canUpdateTime;


	void Start () 
	{
		timeManagerScriptInstance=this;	
	}
	
	void FixedUpdate () 
	{
		if(canUpdateTime)
		{
			if(timerDuration-(Time.time-startTime)<0)
			{
				StopTimer();
				return;
			}
			currentTime=Mathf.Round((timerDuration-(Time.time-startTime))*100f)/100f;
			OnTimerUpdated ();
		}	
	}

	public void StartTimer(float _timerDuration)
	{
		timerDuration=_timerDuration;
		startTime=Time.time;
		canUpdateTime=true;
	}

	void StopTimer()
	{
		timerDuration=0;
		startTime=0;
		canUpdateTime=false;
		OnTimerFinished();
	}

}
