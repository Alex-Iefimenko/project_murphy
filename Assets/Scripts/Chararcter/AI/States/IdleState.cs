using UnityEngine;
using System.Collections;

public class IdleState : StateBase {
	
	private int stateIndex = 0;
	
	public IdleState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return true;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
	}
	
	public override void ExecuteStateActions () 
	{ 
		if (character.Movement.IsMoving == false)
			character.Movement.Walk().ToFurniture(character.Movement.CurrentRoom, "Random");
	}
}
