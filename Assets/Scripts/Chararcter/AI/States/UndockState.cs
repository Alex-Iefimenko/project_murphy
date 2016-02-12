using UnityEngine;
using System.Collections;

public class UndockState : StateBase {
	
	private int stateIndex = 203;
	
	public UndockState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return character.Stats.BasicRoom != null && character.Stats.BasicRoom != character.Movement.CurrentRoom;
	}
	
	public override void Actualize () { 
		base.Actualize ();
		NavigateTo(character.Stats.BasicRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false) 
		{
			character.GObject.transform.parent = character.Stats.BasicRoom.gameObject.transform;
			character.Movement.Purge();
			character.Stats.BasicRoom.Flier.FlyAway(new Vector3(22f, -10f, 0f));
			character.PurgeActions();
		}
	}
}
