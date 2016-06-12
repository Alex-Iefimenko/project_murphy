using UnityEngine;
using System.Collections;

public class IdleState : StateBase {
	
	private new int stateIndex = 0;
	private int tick;

	public override int StateKind { get { return this.stateIndex; } }

	public IdleState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return true;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		tick = Random.Range(3, 7);
	}
	
	public override void Execute () 
	{ 
		base.Execute ();
		if (movement.IsMoving == false && tick > 0)
		{
			tick -= 1;
		}
		else if (movement.IsMoving == false && tick <= 0)
		{
			movement.Walk().ToFurniture(movement.CurrentRoom, "Random");
			tick = Random.Range(3, 7);
		}
	}
}
