using UnityEngine;
using System.Collections;

public class RestState : StateBase {
	
	private int stateIndex = 14;
	private int tick;

	public RestState (CharacterMain character) : base(character) { }

	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		NavigateTo(ShipState.allRooms[(int)Room.RoomTypes.LivingQuarters]);
		tick = Random.Range(7, 10);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.CurrentRoom == ShipState.allRooms[(int)Room.RoomTypes.LivingQuarters]) 
			tick -= 1;
		if (character.Movement.IsMoving() == false)
			character.View.SetSubState(1);
		if (tick <= 0)
			character.PurgeActions();
	}
}