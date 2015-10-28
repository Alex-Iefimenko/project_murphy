using UnityEngine;
using System.Collections;

public class UnconsitiousState : StateBase {
	
	private int stateIndex = 2;
	
	public UnconsitiousState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override void Actualize () { base.Actualize (); }
	
	public override void ExecuteStateActions () 
	{
		if (character.Stats.Health > character.Stats.HealthThreshold) character.PurgeActions();
	}
	
}
