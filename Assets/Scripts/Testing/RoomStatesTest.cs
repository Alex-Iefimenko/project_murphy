using UnityEngine;
using System.Collections;

public class RoomStatesTest : MonoBehaviour {

	public Enums.RoomTypes room = Enums.RoomTypes.Disposal;
	public float amount = 10f;
	public bool start = false;
	

	// Update is called once per frame
	void Update () {
		if (start)
		{
			start = false;
			ShipState.Inst.specRooms[room].SatesHandler.ForceState<FireRoomState>(amount);
		}
	}
}
