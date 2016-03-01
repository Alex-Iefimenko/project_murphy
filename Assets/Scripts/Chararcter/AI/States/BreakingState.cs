using UnityEngine;
using System.Collections;

public class BreakingState : StateBase {
	
	private int stateIndex = 102;
	private int tick;
	
	public BreakingState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		return character.Movement.CurrentRoom.Stats.Locked;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		character.Movement.Walk().ToPoint(character.Movement.CurrentRoom.Objects.DoorExitPoint());
		tick = Random.Range(15, 20);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (character.Movement.IsMoving == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
		}
	}

	public override bool DisableCondition () 
	{
		return tick <= 0;
	}

	public override void Purge ()
	{
		base.Purge ();
		character.Movement.CurrentRoom.Stats.Locked = false;
	}
}
