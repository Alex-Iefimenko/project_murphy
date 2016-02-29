using UnityEngine;
using System.Collections;

public class CleanChemistryState : StateBase {
	
	private int stateIndex = 17;
	
	public CleanChemistryState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.Stats.IsHazardous();
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToPoint(character.Movement.CurrentRoom.Objects.GetRandomRoomPoint());
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.ChemistryClearing(character.Stats.CleanChemistry);
		if (character.Movement.IsMoving == false)
			character.View.SetSubState(1);
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return character.Movement.CurrentRoom.Stats.IsHazardous() == false;
	}
	
}
