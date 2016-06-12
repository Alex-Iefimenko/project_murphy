using UnityEngine;
using System.Collections;

public class SleepState : StateBase {
	
	private new int stateIndex = 10;

	public override int StateKind { get { return this.stateIndex; } }
	
	public SleepState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return stats.IsExhaust;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToFurniture (ShipState.Inst.specRooms[Enums.RoomTypes.LivingQuarters], "Random");
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			stats.Sleep ();
			OnSubStateChange (1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return stats.IsRested;
	}
}

