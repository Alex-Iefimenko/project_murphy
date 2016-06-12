using UnityEngine;
using System.Collections;

public class RoomStatesTest : MonoBehaviour {

	public enum RoomStates { Broken, Fire, RadiationHazard, ChemistryHazard, Unelectryfied, WeatherChange,
							NoGravity, PlantsMutation, Depressurization, Destroyed }
	public RoomStates state = RoomStates.Broken;
	public Enums.RoomTypes room = Enums.RoomTypes.Disposal;
	public float amount = 10f;
	public bool start = false;
	

	// Update is called once per frame
	void Update () {
		if (start)
		{
			start = false;
			switch (state)
			{
			case RoomStates.Broken:
				Ship.Inst.GetRoom(room.ToString()).ForceState<BrokenRoomState>(amount);
				break;
			case RoomStates.Fire:
				Ship.Inst.GetRoom(room.ToString()).ForceState<FireRoomState>(amount); 
				break;
			case RoomStates.RadiationHazard:
				Ship.Inst.GetRoom(room.ToString()).ForceState<RadiationHazardRoomState>(amount); 
				break;
			case RoomStates.ChemistryHazard:
				Ship.Inst.GetRoom(room.ToString()).ForceState<ChemistryHazardRoomState>(amount); 
				break;
			case RoomStates.Unelectryfied:
				Ship.Inst.GetRoom(room.ToString()).ForceState<UnelectryfiedRoomState>(amount); 
				break;
			case RoomStates.WeatherChange:
				Ship.Inst.GetRoom(room.ToString()).ForceState<WeatherChangeRoomState>(amount); 
				break;
			case RoomStates.NoGravity:
				Ship.Inst.GetRoom(room.ToString()).ForceState<NoGravityRoomState>(amount); 
				break;
			case RoomStates.PlantsMutation:
				Ship.Inst.GetRoom(room.ToString()).ForceState<PlantsMutationRoomState>(amount); 
				break;
			case RoomStates.Depressurization:
				Ship.Inst.GetRoom(room.ToString()).ForceState<DepressurizationRoomState>(amount); 
				break;
			case RoomStates.Destroyed:
				Ship.Inst.GetRoom(room.ToString()).ForceState<DestroyedRoomState>(amount); 
				break;
			}

		}
	}
}
