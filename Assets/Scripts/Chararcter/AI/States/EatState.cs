using UnityEngine;
using System.Collections;

public class EatState : StateBase {
	
	private new int stateIndex = 9;

	public override int StateKind { get { return this.stateIndex; } }
	
	public EatState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return stats.IsHungry;
	}

	public override void Actualize () { 
		base.Actualize (); 
		movement.Walk ().ToFurniture (Ship.Inst.GetRoom("Dinnery"), "Random");
	}
	
	public override void Execute ()
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			stats.Eat ();
			OnSubStateChange(1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return stats.IsFull;
	}
}

