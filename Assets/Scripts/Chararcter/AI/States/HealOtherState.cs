using UnityEngine;
using System.Collections;

public class HealOtherState : StateBase {

	private int stateIndex = 8;
	private ICharacter wounded = null;

	public HealOtherState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { 
		base.Actualize (); 
		wounded = character.Movement.CurrentRoom.ContainsWounded(character);
		NavigateTo(wounded);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false && character.Movement.IsNearObject(wounded.GObject))
		{
			character.View.SetSubState(1);
			wounded.Heal(character.Stats.HealOther);
		}
		if (character.Movement.IsMoving() == false && !character.Movement.IsNearObject(wounded.GObject))
		{
			character.PurgeActions();
		}
		if (wounded.Stats.IsWounded() == false)
			character.PurgeActions();
	}
	
}
