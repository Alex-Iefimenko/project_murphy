using UnityEngine;
using System.Collections;

public class IdleState : StateBase {
	
	private int stateIndex = 0;
	private int tick;
	
	public IdleState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		return true;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		tick = Random.Range(3, 7);
	}
	
	public override void ExecuteStateActions () 
	{ 
		if (character.Movement.IsMoving == false && tick > 0)
		{
			tick -= 1;
		}
		else if (character.Movement.IsMoving == false && tick <= 0)
		{
			character.Movement.Walk().ToFurniture(character.Movement.CurrentRoom, "Random");
			tick = Random.Range(3, 7);
		}
	}
}
