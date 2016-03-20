using UnityEngine;
using System.Collections;
using System.Linq;

public class OffItState : StateBase {
	
	private new int stateIndex = 202;

	public override int StateKind { get { return this.stateIndex; } }

	public OffItState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return room.Stats.IsDangerous ();
	}
	
	public override void Actualize () { 
		base.Actualize ();
		Room room = Helpers.GetRandomArrayValue<Room>(movement.CurrentRoom.neighbors.Keys.ToList());
		movement.Run ().ToRoom (room);
	}
	
	public override void Execute () 
	{
		base.Execute ();
	}
	
	public override bool DisableCondition () 
	{
		return movement.IsMoving == false;
	}
}
