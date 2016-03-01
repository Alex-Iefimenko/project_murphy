using UnityEngine;
using System.Collections;
using System.Linq;

public class UndockState : StateBase {
	
	private int stateIndex = 203;

	public UndockState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool EnableCondition (Room room) 
	{
		return character.Stats.BasicRoom != null && character.Stats.BasicRoom != character.Movement.CurrentRoom;
	}
	
	public override void Actualize () {
		base.Actualize ();
		character.Movement.Walk().ToFurniture(character.Stats.BasicRoom, "Random");
	}
	
	public override void ExecuteStateActions () 
	{
		base.ExecuteStateActions ();
	}

	public override bool DisableCondition () 
	{
		return character.Movement.IsMoving == false && GroupGathered ();
	}
	
	public override void Purge ()
	{
		character.Movement.Purge();
		character.Stats.BasicRoom.Flier.FlyAway(new Vector3(22f, -10f, 0f));
		character.PurgeActions();
	}

	private bool GroupGathered ()
	{
		bool groupGathered = true;
		if (character.Coordinator != null)
		{
			ICharacter[] followersCharacters = character.Coordinator.FollowersCharacters
				.Where(v => v != null && !v.Stats.IsUnconscious() && !v.Stats.IsDead()).ToArray();
			ICharacter[] supportCharacters = character.Coordinator.SupportCharacters
				.Where(v => v != null && !v.Stats.IsUnconscious() && !v.Stats.IsDead()).ToArray();
			for (int i = 0; i < followersCharacters.Length; i++) 
				groupGathered = groupGathered && followersCharacters[i].Movement.CurrentRoom == character.Stats.BasicRoom;
			for (int i = 0; i < supportCharacters.Length; i++) 
				groupGathered = groupGathered && supportCharacters[i].Movement.CurrentRoom == character.Stats.BasicRoom;
		}
		return groupGathered;
	}
}
