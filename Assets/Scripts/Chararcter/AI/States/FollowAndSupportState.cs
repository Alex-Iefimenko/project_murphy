using UnityEngine;
using System.Collections;

public class FollowAndSupportState : StateBase {

	private int stateIndex = 204;
	private ICharacter leader = null;
	
	public FollowAndSupportState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		return character.Coordinator != null 
			&& character.Coordinator.leader != null
			&& character.Coordinator.leader != character;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		leader = character.Coordinator.leader;
		character.Movement.Walk().ToCharacter(leader);
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
		if (leader.Movement.IsMoving && character.Movement.Target.GObject != leader.GObject)
		{
			character.Movement.Walk().ToCharacter(leader);
		}
		else if (leader.Movement.IsMoving && !character.Movement.IsMoving && character.Movement.Target.GObject == leader.GObject)
		{
			character.Movement.Walk().ToCharacter(leader);
		}
		else if (!leader.Movement.IsMoving && !character.Movement.IsMoving)
		{
			character.Movement.Walk().ToPoint(character.Movement.CurrentRoom.Objects.GetRandomRoomPoint());
		}
	}
	
	public override bool DisableCondition () 
	{
		return leader == null || !leader.Stats.IsActive();
	}

}
