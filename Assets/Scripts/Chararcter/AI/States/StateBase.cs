using UnityEngine;
using System.Collections;

public class StateBase : IState {

	public ICharacter character;
	private int stateIndex;

	public StateBase (ICharacter newCharacter) 
	{
		character = newCharacter;
	}

	public virtual int StateKind 
	{ 
		get; set; 
	}

	public virtual void Actualize () 
	{
		character.View.SetState(StateKind);
	}

	public virtual bool EnableCondition (Room room) 
	{
		return true;
	}
	
	public virtual bool DisableCondition () 
	{
		return false;
	}

	public virtual void ExecuteStateActions () 
	{
		if (DisableCondition ()) Purge ();
	}

	public virtual void Purge ()
	{
		character.PurgeActions ();
	}
}
