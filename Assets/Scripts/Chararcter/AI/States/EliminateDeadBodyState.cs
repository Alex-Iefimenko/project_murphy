using UnityEngine;
using System.Collections;

public class EliminateDeadBodyState : StateBase {
	
	private int stateIndex = 11;
	private ICharacter dead = null;
	private bool pulling = false;

	public EliminateDeadBodyState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return room.Objects.ContainsDead() != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		dead = character.Movement.CurrentRoom.Objects.ContainsDead();
		dead.Lock = true;
		character.Movement.Walk().ToCharacter(dead);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (!pulling && character.Movement.IsNearObject(dead.GObject))
		{
			character.View.SetSubState(1);
			character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.Disposal], "DisposalPort");
			character.Movement.Pull((IMovable)dead);
			pulling = true;

		}
	}

	public override bool DisableCondition () 
	{
		return pulling && character.Movement.IsMoving == false;
	}

	public override void Purge ()
	{
		base.Purge ();
		dead.Movement.AdjustPostion(new Vector3(-9f, -7f, 1f));
		ShipState.Inst.specRooms[Enums.RoomTypes.Disposal].Objects.Untrack(dead);
		MonoBehaviour.Destroy(dead.GObject, 10f);
		character.PurgeActions();
		//Temporary
		foreach (SpriteRenderer sprite in dead.GObject.GetComponentsInChildren<SpriteRenderer>())
		{
			sprite.sortingOrder = -100;
		}
	}
	
}
