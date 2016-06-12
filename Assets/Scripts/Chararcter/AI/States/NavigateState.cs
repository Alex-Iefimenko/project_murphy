using UnityEngine;
using System.Collections;

public class NavigateState : StateBase {
	
	private new int stateIndex = 5;

	public override int StateKind { get { return this.stateIndex; } }
	
	public NavigateState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public IRoom TargetRoom { get; set; }

	public override bool EnableCondition (IRoom room) 
	{
		return (aiHandler.CurrentState != null && aiHandler.CurrentState.StateKind == 5);
	}

	public override void Actualize () 
	{ 
		base.Actualize (); 
		movement.Run ().ToRoom (TargetRoom);
	}

	public override void Execute () 
	{

		base.Execute ();
	}
	
	public override bool DisableCondition () 
	{
		return movement.IsMoving == false;
	}

	public override void Purge ()
	{
		base.Purge ();
	}
}
