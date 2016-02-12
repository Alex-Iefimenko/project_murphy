using UnityEngine;
using System.Collections;

public class TradeState : StateBase {
	
	private int stateIndex = 201;
	private int tick;
	private bool traded = false;
	
	public TradeState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return !traded;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		NavigateTo(ShipState.Inst.specRooms[Enums.RoomTypes.PowerSource]);
		tick = Random.Range(10, 15);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false)
		{
			tick -= 1;
			character.View.SetSubState(1);
		}
		if (tick <= 0)
		{
			traded = true;
			character.PurgeActions();
		}
	}
}