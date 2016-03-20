using UnityEngine;
using System.Collections;

public class TeamCoordinator {

	private IGroupCharacter leader;
	private Enums.CharacterTypes leaderType;
	private IGroupCharacter[] followersCharacters;
	private IGroupCharacter[] supportCharacters;
	private GameObject target;
	private bool done;
	
	public TeamCoordinator (IGroupCharacter lead, IGroupCharacter[] followers, IGroupCharacter[] support) 
	{
		leader = lead;
		leaderType = leader.Type;
		followersCharacters = followers;
		supportCharacters = support;

		leader.Coordinator.targetChanged += TargetChange;
		leader.Coordinator.missionStateChanged += MissionStateChange;
		leader.Coordinator.Leader = true;
		leader.Coordinator.LeaderCharacter = (ICharacter)leader;
		leader.Coordinator.FollowersCharacters = followers;
		leader.Coordinator.SupportCharacters = support;
		for (int i = 0; i < followersCharacters.Length; i++)
		{
			followersCharacters[i].Coordinator.Leader = false;
			followersCharacters[i].Coordinator.LeaderCharacter = (ICharacter)leader;
			followersCharacters[i].Coordinator.FollowersCharacters = followersCharacters;
			followersCharacters[i].Coordinator.SupportCharacters = supportCharacters;
		}
		for (int i = 0; i < supportCharacters.Length; i++)
		{
			supportCharacters[i].Coordinator.Leader = false;
			supportCharacters[i].Coordinator.LeaderCharacter = (ICharacter)leader;
			supportCharacters[i].Coordinator.FollowersCharacters = followersCharacters;
			supportCharacters[i].Coordinator.SupportCharacters = supportCharacters;
		}
		Broadcaster.Instance.tickEvent += Tick;
	}

	public void TargetChange (GameObject newTarget)
	{
		target = newTarget;
		for (int i = 0; i < followersCharacters.Length; i++) followersCharacters[i].Coordinator.Target = target;
		for (int i = 0; i < supportCharacters.Length; i++) supportCharacters[i].Coordinator.Target = target;
	}

	public void MissionStateChange (bool isDone)
	{
		done = isDone;
		for (int i = 0; i < followersCharacters.Length; i++) followersCharacters[i].Coordinator.Done = done;
		for (int i = 0; i < supportCharacters.Length; i++) supportCharacters[i].Coordinator.Done = done;
	}

	public void Tick ()
	{
		if (leader == null || !leader.IsActive) GetNewLeader ();
	}
	
	private void GetNewLeader ()
	{
		IGroupCharacter newLeader = null;
		for (int i = 0; i < followersCharacters.Length; i++)
		{
			if (followersCharacters[i] != null && followersCharacters[i].IsActive)
			{
				newLeader = followersCharacters[i];
				followersCharacters[i] = leader;
				ChangeLeader (newLeader);
				break;
			}
		}
	}

	private void ChangeLeader (IGroupCharacter newLeader)
	{
		leader.Coordinator.targetChanged -= TargetChange;
		leader.Coordinator.missionStateChanged -= MissionStateChange;
		newLeader.MutateType (leaderType);
		leader = newLeader;
		leader.Coordinator.targetChanged += TargetChange;
		leader.Coordinator.missionStateChanged += MissionStateChange;
	}

}
