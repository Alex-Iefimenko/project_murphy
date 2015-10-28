using UnityEngine;
using System.Collections;

public class EliminateDeadBodyState : StateBase {
	
	private int stateIndex = 11;
	private ICharacter dead = null;
	private bool pulling = false;

	public EliminateDeadBodyState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		dead = character.Movement.CurrentRoom.ContainsDead();
		NavigateTo(dead);
	}
	
	public override void ExecuteStateActions () 
	{
		if (!pulling && character.Movement.IsNearObject(dead.GObject))
		{
			character.View.SetSubState(1);
			//dead.PullBy(character);
			NavigateTo(ShipState.allRooms[(int)Room.RoomTypes.Disposal]);
			pulling = true;
		}
		if (character.Movement.IsMoving() == false)
		{
			//dead.Movement.Transpose();
			character.PurgeActions();
		}

	}
	
}
