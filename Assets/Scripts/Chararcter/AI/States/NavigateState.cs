using UnityEngine;
using System.Collections;

public class NavigateState : StateBase {
	
	private int stateIndex = 5;
	
	public NavigateState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	public Room TargetRoom { get; set; }
	public bool Full { get; set; }

	public override bool CheckCondition (Room room) 
	{
		return (character.AiHandler.CurrentState != null && character.AiHandler.CurrentState.StateKind == 5);
	}

	public override void Actualize () 
	{ 
		base.Actualize (); 
		character.Movement.Navigate(TargetRoom, Full);
	}

	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false) character.PurgeActions();
	}
	
}
