using UnityEngine;
using System.Collections;

public class HealHimselfState : StateBase {
	
	private int stateIndex = 4;
	
	public HealHimselfState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(ShipState.allRooms[(int)Room.RoomTypes.MedBay]);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.CurrentRoom == ShipState.allRooms[(int)Room.RoomTypes.MedBay]) 
			character.Stats.Health += character.Stats.HealthIncrease;
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Stats.Health >= character.Stats.MaxHealth)
			character.PurgeActions();
	}
	
}
