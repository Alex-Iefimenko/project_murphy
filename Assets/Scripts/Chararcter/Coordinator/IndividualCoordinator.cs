using UnityEngine;
using System.Collections;

public class IndividualCoordinator {

	public delegate void TargetChange (GameObject target);
	public event TargetChange targetChanged;
	public delegate void MissionState (bool missionState);
	public event MissionState missionStateChanged;

	private GameObject target;
	private bool done;

	public GameObject Target { get { return target; } set { OnTergetChange (value); target = value; } }
	public bool Done { get { return done; } set { OnMissionStateChange (value); done = value; } }
	public bool Leader { get; set; }
	public ICharacter LeaderCharacter { get; set; }
	public ICharacter[] FollowersCharacters { get; set; }
	public ICharacter[] SupportCharacters { get; set; }

	public IndividualCoordinator ()
	{
		Target = null;
		Done = false;
		Leader = true;
		LeaderCharacter = null;
		FollowersCharacters = new ICharacter[] { };
		SupportCharacters = new ICharacter[] { };
	}

	private void OnTergetChange (GameObject target)
	{
		if (targetChanged != null) targetChanged (target);
	}

	private void OnMissionStateChange (bool missionState)
	{
		if (missionStateChanged != null) missionStateChanged (missionState);
	}
}
