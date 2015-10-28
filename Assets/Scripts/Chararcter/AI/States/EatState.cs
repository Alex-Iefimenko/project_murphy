using UnityEngine;
using System.Collections;

public class EatState : StateBase {
	
	private int stateIndex = 9;
	
	public EatState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(ShipState.allRooms[(int)Room.RoomTypes.Dinnery]);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.CurrentRoom == ShipState.allRooms[(int)Room.RoomTypes.Dinnery]) 
			character.Stats.Fatigue += character.Stats.FatigueIncrease;
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Stats.Fatigue >= character.Stats.MaxFatigue)
			character.PurgeActions();
	}
}

