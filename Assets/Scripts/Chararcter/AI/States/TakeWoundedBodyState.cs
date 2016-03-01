using UnityEngine;
using System.Collections;

public class TakeWoundedBodyState : StateBase {
	
	private int stateIndex = 12;
	private ICharacter unconscious = null;
	private bool pulling = false;
	
	public TakeWoundedBodyState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return room.Objects.ContainsUnconscious() != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		unconscious = character.Movement.CurrentRoom.Objects.ContainsUnconscious();
		unconscious.Lock = true;
		character.Movement.Walk().ToCharacter(unconscious);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (!pulling && character.Movement.IsNearObject(unconscious.GObject))
		{
			character.View.SetSubState(1);
			character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.MedBay], "Random");
			character.Movement.Pull((IMovable)unconscious);
			pulling = true;
		}
		if (pulling && character.Movement.IsMoving == false)
		{
			unconscious.Heal(character.Stats.HealOther);
		}
	}
	
	public override bool DisableCondition () 
	{
		return !unconscious.Stats.IsUnconscious();
	}
	
	public override void Purge ()
	{
		base.Purge ();
		unconscious.PurgeActions();
	}
}
