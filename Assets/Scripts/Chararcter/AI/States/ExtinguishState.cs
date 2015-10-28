using UnityEngine;
using System.Collections;

public class ExtinguishState : StateBase {

	private int stateIndex = 7;
	
	public ExtinguishState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(character.Movement.CurrentRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		character.Movement.CurrentRoom.Extinguish(character.Stats.FireExtinguish);
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (character.Movement.CurrentRoom.State.IsOnFire() == false)
			character.PurgeActions();
	}
	
}
