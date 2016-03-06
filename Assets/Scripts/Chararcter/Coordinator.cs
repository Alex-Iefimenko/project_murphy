using UnityEngine;
using System.Collections;
using System.Linq;

public class Coordinator {

	public ICharacter leader;
	private ICharacter[] followersCharacters;
	private ICharacter[] supportCharacters;
//	private Enums.CharacterSides leaderSide;
	private Enums.CharacterTypes leaderType;

	public GameObject Target { get; set; }
	public bool Done { get; set; }
	public ICharacter Leader { get { return leader; } }
	public ICharacter[] FollowersCharacters { get { return followersCharacters; } }
	public ICharacter[] SupportCharacters { get { return supportCharacters; } }
	
	public Coordinator (ICharacter lead, ICharacter[] followers, ICharacter[] support) 
	{
		leader = lead;
//		leaderSide = leader.Side;
		leaderType = leader.Type;
		followersCharacters = followers;
		supportCharacters = support;
		Broadcaster.Instance.tickEvent += Tick;

	}

	public void Tick ()
	{
		if (leader == null || !leader.Stats.IsActive())
		{
			GetNewLeader ();
			if (leader != null) leader.MutateType(leaderType);
		}
	}

	private void GetNewLeader ()
	{
		ICharacter newLeader = null;
		for (int i = 0; i < followersCharacters.Length; i++)
		{
			ICharacter ch = followersCharacters[i];
			if (ch != null && ch.Stats.IsActive())
			{
				newLeader = ch;
				followersCharacters[i] = leader;
				break;
			}
		}
		leader = newLeader;
	}
}
