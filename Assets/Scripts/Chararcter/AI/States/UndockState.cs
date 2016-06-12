using UnityEngine;
using System.Collections;
using System.Linq;

public class UndockState : StateBase {
	
	private new int stateIndex = 203;

	public override int StateKind { get { return this.stateIndex; } }

	public UndockState (ICharacterAIHandler newHandler, AiStateParams param) : base(newHandler, param)
	{
	}
	
	public override bool EnableCondition (IRoom room)
	{
		return stats.BasicRoom != null && stats.BasicRoom != movement.CurrentRoom;
	}
	
	public override void Actualize ()
	{
		base.Actualize ();
		movement.Walk ().ToFurniture (stats.BasicRoom, "Random");
	}
	
	public override void Execute ()
	{
		base.Execute ();
	}

	public override bool DisableCondition ()
	{
		return movement.IsMoving == false && GroupGathered ();
	}
	
	public override void Purge ()
	{
		base.Purge ();
		stats.BasicRoom.FlyAway (new Vector3 (22f, -10f, 0f));
	}

	private bool GroupGathered ()
	{
		bool groupGathered = true;
		ICharacter[] followersCharacters = coordinator.FollowersCharacters.Where (v => v != null && v.IsActive).ToArray ();
		ICharacter[] supportCharacters = coordinator.SupportCharacters.Where (v => v != null && v.IsActive).ToArray ();
		for (int i = 0; i < followersCharacters.Length; i++) 
			groupGathered = groupGathered && followersCharacters [i].CurrentRoom == stats.BasicRoom;
		for (int i = 0; i < supportCharacters.Length; i++) 
			groupGathered = groupGathered && supportCharacters [i].CurrentRoom == stats.BasicRoom;
		return groupGathered;
	}
}
