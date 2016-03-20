using UnityEngine;
using System.Collections;

public class UnconsitiousState : StateBase {
	
	private new int stateIndex = 2;

	public override int StateKind { get { return this.stateIndex; } }
	
	public UnconsitiousState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return stats.IsUnconscious;
	}

	public override void Actualize () { base.Actualize (); }
	
	public override void Execute () 
	{
		base.Execute ();
	}
	
	public override bool DisableCondition () 
	{
		return stats.IsHealthy;
	}
	
}
