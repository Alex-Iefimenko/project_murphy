using UnityEngine;
using System.Collections;

public class DeadState : StateBase {
	
	private new int stateIndex = 1;

	public override int StateKind { get { return this.stateIndex; } }
	
	public DeadState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return stats.IsDead;
	}

	public override void Actualize () { base.Actualize (); }
	
	public override void Execute () { }
	
}
