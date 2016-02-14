using UnityEngine;
using System.Collections;
using System.Linq;

public class UndockState : StateBase {
	
	private int stateIndex = 203;

	public UndockState (CharacterMain character) : base(character) { }
	
	public override int StateKind { get { return stateIndex; } }
	
	public override bool CheckCondition (Room room) 
	{
		return character.Stats.BasicRoom != null && character.Stats.BasicRoom != character.Movement.CurrentRoom;
	}
	
	public override void Actualize () {
		base.Actualize ();
		NavigateTo(character.Stats.BasicRoom);
	}
	
	public override void ExecuteStateActions () 
	{
		if (character.Movement.IsMoving() == false && GroupGathered ()) 
		{
			character.Movement.Purge();
			character.Stats.BasicRoom.Flier.FlyAway(new Vector3(22f, -10f, 0f));
			character.PurgeActions();
		}
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
