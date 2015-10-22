using UnityEngine;
using System.Collections;

public class StateBase : MonoBehaviour, IState {

	public ICharacter character;
	private int stateIndex;

	public StateBase (CharacterMain newCharacter) 
	{
		character = newCharacter;
	}

	public virtual int StateKind { get; set; }

	public virtual void ExecuteStateActions () {}

	public virtual void Actualize () 
	{
		character.PurgeActions();
		character.View.SetState(StateKind);
	}

	public void NavigateTo (Room room)
	{
		character.Movement.NavigateTo(room);
	}

	public void NavigateTo (CharacterMain character)
	{
		character.Movement.NavigateTo(character);
	}

	public void NavigateTo (Room room, Furniture item)
	{
		character.Movement.NavigateTo(room, item);
	}

}
