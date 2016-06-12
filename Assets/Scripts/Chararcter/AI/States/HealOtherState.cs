using UnityEngine;
using System.Collections;

public class HealOtherState : StateBase {

	private new int stateIndex = 8;
	private ICharacter wounded = null;

	public override int StateKind { get { return this.stateIndex; } }

	public HealOtherState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.ContainsWounded(movement.GObject, stats.Side) != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		wounded = movement.CurrentRoom.ContainsWounded(movement.GObject, stats.Side);
		wounded.Lock = true;
		movement.Run ().ToCharacter (wounded);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false && movement.IsNearObject(wounded.GObject))
		{
			OnSubStateChange(1);
			wounded.Heal(stats.HealOther);
		}
	}
	
	public override bool DisableCondition () 
	{
		return wounded.IsHealthy == true || (movement.IsMoving == false && !movement.IsNearObject(wounded.GObject));
	}

	public override void Purge ()
	{
		base.Purge ();
		wounded.Lock = false;
	}

}
