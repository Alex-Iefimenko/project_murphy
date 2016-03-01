using UnityEngine;
using System.Collections;

public class FirstAidState : StateBase {
	
	private int stateIndex = 18;
	
	public FirstAidState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		bool result = 
			character.Stats.Health < character.Stats.HealthThreshold && 
			character.Movement.CurrentRoom == ShipState.Inst.specRooms[Enums.RoomTypes.MedBay];
		return result;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.MedBay], "Random");
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
