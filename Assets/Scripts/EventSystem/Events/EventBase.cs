using UnityEngine;
using System.Collections;

public class EventBase : MonoBehaviour {

	public readonly float amount = 20f;
	public readonly int attempts = 5;

	public void StartEvent () 
	{
		int i = 0;
		while (i++ < attempts) 
		{
			bool result = TryEvent ();
			if (result) break;
		}
	}

	public virtual void StopEvent () { }

	public virtual bool TryEvent () { return true; }

}
