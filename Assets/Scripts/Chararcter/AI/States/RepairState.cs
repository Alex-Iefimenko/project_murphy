using UnityEngine;
using System.Collections;

public class RepairState : StateBase {

	private int stateIndex = 6;
	
	public RepairState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return room.Stats.IsBroken();	
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToFurniture(character.Movement.CurrentRoom, "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		character.Movement.CurrentRoom.Repair(character.Stats.Repair);
		if (character.Movement.IsMoving == false)
			character.View.SetSubState(1);
	}
	
	public override bool DisableCondition () 
	{
		return character.Movement.CurrentRoom.Stats.IsBroken() == false;
	}

}
