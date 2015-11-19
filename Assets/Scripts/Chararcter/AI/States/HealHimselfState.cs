using UnityEngine;
using System.Collections;

public class HealHimselfState : StateBase {
	
	private int stateIndex = 4;
	
	public HealHimselfState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return character.Stats.Health < character.Stats.HealthThreshold;
	}

	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(ShipState.Inst.specRooms[Room.RoomTypes.MedBay]);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false)
		{
			character.Stats.Health += character.Stats.HealthIncrease;
			character.View.SetSubState(1);
		}
		if (character.Stats.Health >= character.Stats.MaxHealth)
			character.PurgeActions();
	}
	
}
