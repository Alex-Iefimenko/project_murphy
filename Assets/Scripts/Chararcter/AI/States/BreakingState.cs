using UnityEngine;
using System.Collections;

public class BreakingState : StateBase {
	
	private int stateIndex = 102;
	private int tick;
	
	public BreakingState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
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
		if (character.Movement.IsMoving == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
		}
		if (tick <= 0)
		{
			character.Movement.CurrentRoom.Stats.Locked = false;
			character.PurgeActions();
		}
	}
}
