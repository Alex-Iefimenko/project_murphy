using UnityEngine;
using System.Collections;

public class RestState : StateBase {
	
	private new int stateIndex = 14;
	private int tick;

	public override int StateKind { get { return this.stateIndex; } }

	public RestState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }

	public override bool EnableCondition (Room room) 
	{
		return true;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToFurniture (ShipState.Inst.specRooms[Enums.RoomTypes.LivingQuarters], "Random");
		tick = Random.Range(7, 10);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			tick -= 1;
			OnSubStateChange(1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return tick <= 0;
	}
}
