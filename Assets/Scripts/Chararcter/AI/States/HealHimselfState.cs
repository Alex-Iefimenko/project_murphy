using UnityEngine;
using System.Collections;

public class HealHimselfState : StateBase {
	
	private int stateIndex = 4;
	
	public HealHimselfState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return character.Stats.Health < character.Stats.HealthThreshold;
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Run().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.MedBay], "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (character.Movement.IsMoving == false)
		{
			character.Stats.Health += character.Stats.HealthIncrease;
			character.Stats.HealthReduction = 0f;
			character.View.SetSubState(1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return character.Stats.Health >= character.Stats.MaxHealth;
	}
	
}
