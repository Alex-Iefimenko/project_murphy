using UnityEngine;
using System.Collections;

public class CharacterGroupCreater : MonoBehaviour {

	public GameObject leader;
	public GameObject[] mainFollowers;
	public GameObject[] supportFollowers;

	public void CreateCharacters ()
	{
		IRoom room = ShipState.Inst.RoomByPoint(transform.position);
		GameObject lead = Instantiate(leader, room.GetRandomRoomPoint(), transform.rotation) as GameObject;


		IGroupCharacter groupLeader = lead.GetComponent<CharacterCreater>().CreateCharacter();
		IGroupCharacter[] groupFollowers = CreateGroup(room, mainFollowers);
		IGroupCharacter[] groupSupport = CreateGroup(room, supportFollowers);
		new TeamCoordinator(groupLeader, groupFollowers, groupSupport);
		Destroy(this.gameObject);
	}

	private IGroupCharacter[] CreateGroup (IRoom room, GameObject[] group)
	{
		IGroupCharacter[] createdGroup = new IGroupCharacter[group.Length];
		for (int i = 0; i < group.Length; i++)
		{
			GameObject character = Instantiate(group[i], room.GetRandomRoomPoint(), transform.rotation) as GameObject;
			createdGroup[i] = character.GetComponent<CharacterCreater>().CreateCharacter();
		}
		return createdGroup;
	}

}
