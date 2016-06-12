using UnityEngine;
using System.Collections;

public class HealHimselfState : StateBase {
	
	private new int stateIndex = 4;

	public override int StateKind { get { return this.stateIndex; } }

	public HealHimselfState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return stats.IsHeavyWounded;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Run ().ToFurniture (Ship.Inst.GetRoom("MedBay"), "Random");
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			stats.Heal (stats.HealthIncrease);
			OnSubStateChange (1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return stats.IsHealthy;
	}
	
}
