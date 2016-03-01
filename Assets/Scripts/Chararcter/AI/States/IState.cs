using UnityEngine;
using System.Collections;

public interface IState
{
	int StateKind { get; }

	void Actualize ();

	void ExecuteStateActions ();

	bool EnableCondition (Room room);
	
}
