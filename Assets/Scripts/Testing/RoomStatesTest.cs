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
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<BrokenRoomState>(amount);
				break;
			case RoomStates.Fire:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<FireRoomState>(amount); 
				break;
			case RoomStates.RadiationHazard:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<RadiationHazardRoomState>(amount); 
				break;
			case RoomStates.ChemistryHazard:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<ChemistryHazardRoomState>(amount); 
				break;
			case RoomStates.Unelectryfied:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<UnelectryfiedRoomState>(amount); 
				break;
			case RoomStates.WeatherChange:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<WeatherChangeRoomState>(amount); 
				break;
			case RoomStates.NoGravity:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<NoGravityRoomState>(amount); 
				break;
			case RoomStates.PlantsMutation:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<PlantsMutationRoomState>(amount); 
				break;
			case RoomStates.Depressurization:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<DepressurizationRoomState>(amount); 
				break;
			case RoomStates.Destroyed:
				ShipState.Inst.specRooms[room].SatesHandler.ForceState<DestroyedRoomState>(amount); 
				break;
			}

		}
	}
}
