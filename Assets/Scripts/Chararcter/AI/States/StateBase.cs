using UnityEngine;
using System.Collections;

public class StateBase : IState {

	protected ICharacterAIHandler aiHandler;
	protected ICharacterStatePrivate stats;
	protected IMovement movement;
	protected IndividualCoordinator coordinator;
	protected int stateIndex;

	public virtual int StateKind { get { return this.stateIndex; } }

	public event ViewStateHandler SubStateChange;
	public event ViewBoolHandler SetCustomBool;

	public StateBase (ICharacterAIHandler newHandler, AiStateParams param) 
	{
		aiHandler = newHandler;
		stats = param.Stats;
		movement = param.Movement;
		coordinator = param.Coordinator;
	}

	protected void OnSubStateChange (int state)
	{
		if (SubStateChange != null) SubStateChange (state);
	}
	
	protected void OnSetCustomBool (string name, bool state)
	{
		if (SetCustomBool != null) SetCustomBool (name, state);
	}

	public virtual void Actualize () 
	{
		OnSubStateChange (0);
	}

	public bool EnableCondition () 
	{
		return EnableCondition (movement.CurrentRoom);
	}
	
	public virtual bool EnableCondition (Room room) 
	{
		return true;
	}
	
	public virtual bool DisableCondition () 
	{
		return false;
	}

	public virtual void Execute () 
	{
		if (DisableCondition ()) Purge ();
	}

	public virtual void Purge ()
	{
		movement.Purge ();
		stats.Purge ();
		aiHandler.PurgeState ();
	}
}
