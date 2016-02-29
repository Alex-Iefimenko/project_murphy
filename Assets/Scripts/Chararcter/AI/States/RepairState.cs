using UnityEngine;
using System.Collections;

public class RepairState : StateBase {

	private int stateIndex = 6;
	
	public RepairState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.Stats.IsBroken();	
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToFurniture(character.Movement.CurrentRoom, "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.Repair(character.Stats.Repair);
		if (character.Movement.IsMoving == false)
			character.View.SetSubState(1);
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return character.Movement.CurrentRoom.Stats.IsBroken() == false;
	}

}
