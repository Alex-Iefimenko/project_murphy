using UnityEngine;
using System.Collections;

public class SleepState : StateBase {
	
	private int stateIndex = 10;
	
	public SleepState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return character.Stats.Sanity <= character.Stats.SanityThreshold;	
	}

	public override void Actualize () { 
		base.Actualize (); 
		character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.LivingQuarters], "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving == false)
		{
			character.Stats.Sanity += character.Stats.SanityIncrease;
			character.View.SetSubState(1);
		}
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return character.Stats.Sanity >= character.Stats.MaxSanity;
	}
}

