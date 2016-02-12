using UnityEngine;
using System.Collections;
using System.Linq;

public class OffItState : StateBase {
	
	private int stateIndex = 202;
	
	public OffItState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return room.Stats.IsDangerous();
	}
	
	public override void Actualize () { 
		base.Actualize ();
		Room room = Helpers.GetRandomArrayValue<Room>(character.Movement.CurrentRoom.neighbors.Keys.ToList());
		NavigateTo(room);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false) character.PurgeActions();
	}
}
