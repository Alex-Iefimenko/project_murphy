using UnityEngine;
using System.Collections;

public class TakeWoundedBodyState : StateBase {
	
	private int stateIndex = 12;
	private ICharacter unconscious = null;
	private bool pulling = false;
	
	public TakeWoundedBodyState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
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
		if (!unconscious.Stats.IsUnconscious())
		{
			unconscious.PurgeActions();
			character.PurgeActions();
		}
	}
}
