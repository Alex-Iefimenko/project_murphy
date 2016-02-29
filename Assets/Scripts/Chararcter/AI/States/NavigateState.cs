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
		character.Movement.Run().ToRoom(TargetRoom);
	}

	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return character.Movement.IsMoving == false;
	}

}
