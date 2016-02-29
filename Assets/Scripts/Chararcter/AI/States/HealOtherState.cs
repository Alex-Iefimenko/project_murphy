using UnityEngine;
using System.Collections;

public class HealOtherState : StateBase {

	private int stateIndex = 8;
	private ICharacter wounded = null;

	public HealOtherState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool CheckCondition (Room room) 
	{
		return room.Objects.ContainsWounded(character) != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		wounded = character.Movement.CurrentRoom.Objects.ContainsWounded(character);
		wounded.Lock = true;
		character.Movement.Run().ToCharacter(wounded);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving == false && character.Movement.IsNearObject(wounded.GObject))
		{
			character.View.SetSubState(1);
			wounded.Heal(character.Stats.HealOther);
		}
		base.ExecuteStateActions ();
	}
	
	public override bool PurgeCondition () 
	{
		return wounded.Stats.IsWounded() == false || 
			character.Movement.IsMoving == false && !character.Movement.IsNearObject(wounded.GObject);
	}
	
}
