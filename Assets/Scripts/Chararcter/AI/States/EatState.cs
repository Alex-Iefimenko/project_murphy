using UnityEngine;
using System.Collections;

public class EatState : StateBase {
	
	private int stateIndex = 9;
	
	public EatState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return character.Stats.Fatigue <= character.Stats.FatigueThreshold;
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.Dinnery], "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (character.Movement.IsMoving == false)
		{
			character.Stats.Fatigue += character.Stats.FatigueIncrease;
			character.View.SetSubState(1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return character.Stats.Fatigue >= character.Stats.MaxFatigue;
	}
}

