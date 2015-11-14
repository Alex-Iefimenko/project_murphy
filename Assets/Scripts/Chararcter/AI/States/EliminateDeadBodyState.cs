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
			character.Movement.Anchor(dead.GObject);
			NavigateTo(ShipState.Inst.specRooms[Room.RoomTypes.Disposal]);
			pulling = true;
		}
		if (character.Movement.IsMoving() == false)
		{
			dead.Movement.AdjustPostion(new Vector3(-9f, -7f, 1f));
			ShipState.Inst.specRooms[Room.RoomTypes.Disposal].Untrack(dead);
			MonoBehaviour.Destroy(dead.GObject, 10f);
			character.PurgeActions();
			//Temporary
			foreach (SpriteRenderer sprite in dead.GObject.GetComponentsInChildren<SpriteRenderer>())
			{
				sprite.sortingOrder = -5;
			}
		}

	}
	
}
