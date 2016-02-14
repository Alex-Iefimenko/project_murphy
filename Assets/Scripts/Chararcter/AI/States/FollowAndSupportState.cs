using UnityEngine;
using System.Collections;

public class FollowAndSupportState : StateBase {

	private int stateIndex = 204;
	private ICharacter leader = null;
	
	public FollowAndSupportState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return character.Coordinator != null 
			&& character.Coordinator.leader != null
			&& character.Coordinator.leader != character;
	}
	
	public override void Actualize () { 
		base.Actualize (); 
		leader = character.Coordinator.leader;
		NavigateTo(leader);
	}
	
	public override void ExecuteStateActions () 
	{
		if (leader.Movement.IsMoving() && character.Movement.Target != leader.GObject)
		{
			NavigateTo(leader);
		}
		else if (leader.Movement.IsMoving() && !character.Movement.IsMoving() && character.Movement.Target == leader.GObject)
		{
			NavigateTo(leader);
		}
		else if (!leader.Movement.IsMoving() && !character.Movement.IsMoving())
		{
			NavigateTo(leader.Movement.CurrentRoom);
		}
		if (leader == null || leader.Stats.IsUnconscious() || leader.Stats.IsDead())
		{
			character.PurgeActions();
		}
	}

}
