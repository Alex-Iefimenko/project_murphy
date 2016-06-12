using UnityEngine;
using System.Collections;

public class EliminateDeadBodyState : StateBase {
	
	private new int stateIndex = 11;
	private ICharacter dead = null;
	private bool pulling = false;

	public override int StateKind { get { return this.stateIndex; } }

	public EliminateDeadBodyState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (IRoom room) 
	{
		return room.ContainsDead != null;
	}

	public override void Actualize () { 
		base.Actualize (); 
		pulling = false;
		dead = movement.CurrentRoom.ContainsDead;
		dead.Lock = true;
		movement.Walk ().ToCharacter (dead);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (!pulling && movement.IsNearObject(dead.GObject))
		{
			OnSubStateChange (1);
			movement.Walk ().ToFurniture(Ship.Inst.GetRoom("Disposal"), "DisposalPort");
			movement.Pull ((IMovable)dead);
			pulling = true;
		}
	}

	public override bool DisableCondition () 
	{
		return pulling && movement.IsMoving == false;
	}

	public override void Purge ()
	{
		base.Purge ();
		dead.Push (new Vector3(-9f, -7f, 1f));
		dead.Vanish ();
		dead.ChangeLayer (-5);
	}
	
}
