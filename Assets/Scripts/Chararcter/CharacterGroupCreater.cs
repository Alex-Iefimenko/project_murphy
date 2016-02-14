using UnityEngine;
using System.Collections;

public class CharacterGroupCreater : MonoBehaviour {

	public GameObject leader;
	public GameObject[] mainFollowers;
	public GameObject[] supportFollowers;

	public void CreateCharacters ()
	{
		Room room = ShipState.Inst.RoomByPoint(transform.position);
		GameObject lead = Instantiate(leader, room.Objects.GetRandomRoomPoint(), transform.rotation) as GameObject;
		ICharacter groupLeader = lead.GetComponent<CharacterCreater>().CreateCharacter();
		ICharacter[] groupFollowers = CreateGroup(room, mainFollowers);
		ICharacter[] groupSupport = CreateGroup(room, supportFollowers);
		Coordinator groupCoordinator = new Coordinator(groupLeader, groupFollowers, groupSupport);
		groupLeader.Coordinator = groupCoordinator;
		for (int i = 0; i < groupFollowers.Length; i++) groupFollowers[i].Coordinator = groupCoordinator;
		for (int i = 0; i < groupSupport.Length; i++) groupFollowers[i].Coordinator = groupCoordinator;
		Destroy(this.gameObject);
	}

	private ICharacter[] CreateGroup (Room room, GameObject[] group)
	{
		ICharacter[] createdGroup = new ICharacter[group.Length];
		for (int i = 0; i < group.Length; i++)
		{
			GameObject character = Instantiate(group[i], room.Objects.GetRandomRoomPoint(), transform.rotation) as GameObject;
			createdGroup[i] = character.GetComponent<CharacterCreater>().CreateCharacter();
		}
		return createdGroup;
	}

}
