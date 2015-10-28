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
		character.PurgeActions();
		character.View.SetState(StateKind);
	}

	public virtual void ExecuteStateActions () {}

	public void NavigateTo (Room room)
	{
		character.Movement.NavigateTo(room);
	}

	public void NavigateTo (ICharacter targetCharacter)
	{
		character.Movement.NavigateTo(targetCharacter);
	}

	public void NavigateTo (Room room, Furniture item)
	{
		character.Movement.NavigateTo(room, item);
	}

}
