using UnityEngine;
using System.Collections;

public class FirstAidState : StateBase {
	
	private new int stateIndex = 18;

	public override int StateKind { get { return this.stateIndex; } }
	
	public FirstAidState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		bool result = stats.IsWounded && movement.CurrentRoom == ShipState.Inst.specRooms[Enums.RoomTypes.MedBay];
		return result;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToFurniture (movement.CurrentRoom, "Random");
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			stats.Heal (stats.HealthIncrease);
			OnSubStateChange(1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return stats.IsHealthy;
	}
}
