using UnityEngine;
using System.Collections;

public class TakeWoundedBodyState : StateBase {
	
	private new int stateIndex = 12;
	private ICharacter unconscious = null;
	private bool pulling = false;

	public override int StateKind { get { return this.stateIndex; } }

	public TakeWoundedBodyState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.ContainsUnconscious != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		unconscious = movement.CurrentRoom.ContainsUnconscious;
		unconscious.Lock = true;
		movement.Walk ().ToCharacter (unconscious);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (!pulling && movement.IsNearObject(unconscious.GObject))
		{
			OnSubStateChange (1);
			movement.Walk ().ToFurniture (ShipState.Inst.specRooms[Enums.RoomTypes.MedBay], "Random");
			movement.Pull ((IMovable)unconscious);
			pulling = true;
		}
		if (pulling && movement.IsMoving == false)
		{
			unconscious.Heal(stats.HealOther);
		}
	}
	
	public override bool DisableCondition () 
	{
		return !unconscious.IsUnconscious;
	}
	
	public override void Purge ()
	{
		base.Purge ();
	}
}
