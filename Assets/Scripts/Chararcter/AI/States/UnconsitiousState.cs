using UnityEngine;
using System.Collections;

public class UnconsitiousState : StateBase {
	
	private int stateIndex = 2;
	
	public UnconsitiousState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }

	public override bool EnableCondition (Room room) 
	{
		return character.Stats.IsUnconscious();
	}

	public override void Actualize () { base.Actualize (); }
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
	}
	
	public override bool DisableCondition () 
	{
		return character.Stats.Health >= character.Stats.MaxHealth;
	}
	
}
