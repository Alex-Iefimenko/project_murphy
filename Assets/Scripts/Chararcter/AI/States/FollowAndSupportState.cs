using UnityEngine;
using System.Collections;

public class FollowAndSupportState : StateBase {

	private new int stateIndex = 204;
	private ICharacter leader = null;

	public override int StateKind { get { return this.stateIndex; } }
	
	public FollowAndSupportState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param) { }
	
	public override bool EnableCondition (Room room) 
	{
		return coordinator.Leader == false;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		leader = coordinator.LeaderCharacter;
		movement.Walk ().ToCharacter (leader);
	}
	
	public override void Execute () 
	{
		base.Execute ();
		if (leader.IsMoving && movement.Target.GObject != leader.GObject)
		{
			movement.Walk ().ToCharacter (leader);
		}
		else if (leader.IsMoving && !movement.IsMoving && movement.Target.GObject == leader.GObject)
		{
			movement.Walk ().ToCharacter (leader);
		}
		else if (!leader.IsMoving && !movement.IsMoving)
		{
			movement.Walk ().ToPoint (movement.CurrentRoom.Objects.GetRandomRoomPoint());
		}
	}
	
	public override bool DisableCondition () 
	{
		return leader == null || !leader.IsActive;
	}

}
