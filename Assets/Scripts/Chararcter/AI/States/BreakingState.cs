using UnityEngine;
using System.Collections;

public class BreakingState : StateBase {
	
	private new int stateIndex = 102;
	private int tick;

	public override int StateKind { get { return this.stateIndex; } }

	public BreakingState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return movement.CurrentRoom.Stats.Locked;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		movement.Walk().ToPoint(movement.CurrentRoom.Objects.DoorExitPoint());
		tick = Random.Range(15, 20);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			tick -= 1;
			OnSubStateChange (1);
		}
	}

	public override bool DisableCondition () 
	{
		return tick <= 0;
	}

	public override void Purge ()
	{
		base.Purge ();
		movement.CurrentRoom.Stats.Locked = false;
	}
}
