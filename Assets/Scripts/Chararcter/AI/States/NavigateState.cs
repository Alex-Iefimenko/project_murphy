using UnityEngine;
using System.Collections;

public class NavigateState : StateBase {
	
	private int stateIndex = 5;
	
	public NavigateState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return (character.AiHandler.CurrentState != null && character.AiHandler.CurrentState.StateKind == 5);
	}

	public override void Actualize () { base.Actualize (); }

	public void Actualize (Room room) { 
		Actualize ();
		character.Movement.Navigate(room);
	}

	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false) character.PurgeActions();
	}
	
}
