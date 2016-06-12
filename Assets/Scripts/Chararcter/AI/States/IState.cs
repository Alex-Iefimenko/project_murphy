using UnityEngine;
using System.Collections;

public delegate void ViewStateHandler (int state);

public delegate void ViewBoolHandler (string name, bool customBool);

public interface IState
{
	int StateKind { get; }

	event ViewStateHandler SubStateChange;

	event ViewBoolHandler SetCustomBool;

	void Actualize ();

	void Execute ();

	bool EnableCondition ();

	bool EnableCondition (IRoom room);

	void Purge ();
	
}
