using UnityEngine;
using System.Collections;

public class TradeState : StateBase {
	
	private new int stateIndex = 201;
	private int tick;
	private bool traded = false;

	public override int StateKind { get { return this.stateIndex; } }

	public TradeState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return !traded;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		movement.Walk ().ToFurniture (ShipState.Inst.specRooms[Enums.RoomTypes.Dinnery], "Random");
		tick = Random.Range(10, 15);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (movement.IsMoving == false)
		{
			tick -= 1;
			OnSubStateChange (1);
		}
	}
	
	public override bool DisableCondition () 
	{
		return tick <= 0;
	}
	
	public override void Purge ()
	{
		base.Purge ();
		traded = true;
	}
}