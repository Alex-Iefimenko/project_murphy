using UnityEngine;
using System.Collections;

public class SleepState : StateBase {
	
	private int stateIndex = 10;
	
	public SleepState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(ShipState.allRooms[(int)Room.RoomTypes.LivingQuarters]);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.CurrentRoom == ShipState.allRooms[(int)Room.RoomTypes.LivingQuarters]) 
			character.Stats.Sanity += character.Stats.SanityIncrease;
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Stats.Sanity >= character.Stats.MaxSanity)
			character.PurgeActions();
	}
}

