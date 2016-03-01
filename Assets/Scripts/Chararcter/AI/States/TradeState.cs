using UnityEngine;
using System.Collections;

public class TradeState : StateBase {
	
	private int stateIndex = 201;
	private int tick;
	private bool traded = false;
	
	public TradeState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		return !traded;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		character.Movement.Walk().ToFurniture(ShipState.Inst.specRooms[Enums.RoomTypes.Dinnery], "Random");
		tick = Random.Range(10, 15);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (character.Movement.IsMoving == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
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