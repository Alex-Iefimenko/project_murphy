using UnityEngine;
using System.Collections;

public class StateBase : IState {

	public ICharacter character;
	private int stateIndex;

	public StateBase (ICharacter newCharacter) 
	{
		character = newCharacter;
	}

	public virtual int StateKind { get; set; }

	public virtual void Actualize () 
	{
		character.View.SetState(StateKind);
	}

	public virtual bool CheckCondition (Room room) 
	{
		return true;
	}

	public virtual void ExecuteStateActions () 
	{
		if (PurgeCondition ()) character.PurgeActions ();
	}

	public virtual bool PurgeCondition () 
	{
		return false;
	}
}
