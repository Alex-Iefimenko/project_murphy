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
		return room.ContainsUnconscious() != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		unconscious = character.Movement.CurrentRoom.ContainsUnconscious();
		unconscious.Lock = true;
		NavigateTo(unconscious);
	}
	
	public override void ExecuteStateActions () 
	{
		if (!pulling && character.Movement.IsNearObject(unconscious.GObject))
		{
			character.View.SetSubState(1);
			character.Movement.Anchor(unconscious.GObject);
			NavigateTo(ShipState.Inst.specRooms[Enums.RoomTypes.MedBay]);
			pulling = true;
		}
		if (pulling && character.Movement.IsMoving() == false)
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
