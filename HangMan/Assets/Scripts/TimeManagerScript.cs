using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerScript : MonoBehaviour {

	public static TimeManagerScript instance;
	
	public float currentTime;

	public delegate void TimerFinished();
	public TimerFinished OnTimerFinished;

	public delegate void TimerUpdate();
	public TimerUpdate OnTimerUpdated ;

	private float timerDuration,startTime;
	private bool canUpdateTime;
	

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);
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

	public void StopTimer()
	{
		timerDuration=0;
		startTime=0;
		canUpdateTime=false;
		OnTimerFinished();
	}

}
