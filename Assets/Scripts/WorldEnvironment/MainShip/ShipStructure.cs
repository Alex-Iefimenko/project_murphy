using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipStructure  {

	private Room[] roomList;
	private Dictionary<Room, Neighbor[]> shipGraph = new Dictionary<Room, Neighbor[]>();

	public ShipStructure (Room[] rooms)
	{
		roomList = rooms;
		for (int i = 0; i < roomList.Length; i++)
		{
			Neighbor[] roomNeighbors = new Neighbor[roomList[i].neighbors.Count];
			roomList[i].neighbors.Values.CopyTo(roomNeighbors, 0);
			shipGraph.Add(roomList[i], roomNeighbors);
		}
	}

	// Returns list of world points for NPC traveling
	// https://gist.github.com/joninvski/701720
	public List<Vector3> GetStepsToRoom (Room initialRoom, Room targetRoom) {
		// Initializing
		Dictionary<Room, float> distances = new Dictionary<Room, float>();
		Dictionary<Room, Room> predecessors = new Dictionary<Room, Room>();

		for (int i = 0; i < roomList.Length; i++)
		{
			distances[roomList[i]] = float.PositiveInfinity;
			predecessors[roomList[i]] = null;
		}
		distances[initialRoom] = 0f;
					

		// Bellman-Ford algorithm itself
		for (int i = 0; i < shipGraph.Count; i++)
		{
			for (int j = 0; j < roomList.Length; j++)
			{
				Neighbor[] neighbors = shipGraph[roomList[j]];
				for (int k = 0; k < neighbors.Length; k++)
				{
					if (distances[neighbors[k].Room] > distances[roomList[j]] + neighbors[k].Distance)
					{
						distances[neighbors[k].Room] = distances[roomList[j]] + neighbors[k].Distance;
						predecessors[neighbors[k].Room] = roomList[j];
					}
				}
			}
		}

		// Restoring of closest path
		if (distances[targetRoom] == float.PositiveInfinity)
		{
			return null;
		}
		else
		{
			return RestoreClosesPath (targetRoom, predecessors);
		}
	}

	private List<Vector3> RestoreClosesPath (Room tRoom, Dictionary<Room, Room> predecessors)
	{
		List<Vector3> result = new List<Vector3>();
		Room stepRoom = tRoom;
		Room nextStepRoom;
		result.Add (tRoom.transform.position);
		while (stepRoom != null)
		{
			nextStepRoom = predecessors[stepRoom];
			if (nextStepRoom != null) 
			{ 
				result.Add (stepRoom.neighbors[nextStepRoom].ExitPoint);
				result.Add (stepRoom.neighbors[nextStepRoom].EntrancePoint);
			}
			stepRoom = nextStepRoom;
		}
		
		result.Reverse();
		return result;
	}

}
