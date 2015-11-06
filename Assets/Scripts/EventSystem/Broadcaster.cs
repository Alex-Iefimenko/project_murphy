using UnityEngine;
using System.Collections;

public class Broadcaster {

	private static Broadcaster instance;
	private static readonly object locker = new object();
	public delegate void MultiDelegate(); 
	public event MultiDelegate tickEvent;
	public event MultiDelegate actionEvent;
	public event MultiDelegate dayEvent;

	private Broadcaster() { }

	public static Broadcaster Instance {
		get
		{
			lock (locker)
			{
				if (instance == null) instance = new Broadcaster();
			}
			return instance;
		}
	}

	// Broadcast Tick
	public void BroadcastTick () {
		if (tickEvent != null) tickEvent();
	}

	// Broadcast Action
	public void BroadcastAction () {
		if (actionEvent != null) actionEvent();
	}

	// Broadcast Day
	public void BroadcastDay () {
		if (dayEvent != null) dayEvent();
	}
}
