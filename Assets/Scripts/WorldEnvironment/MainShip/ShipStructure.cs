using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipStructure  {

	private IRoom[] roomList;
	private Dictionary<IRoom, Neighbor[]> shipGraph = new Dictionary<IRoom, Neighbor[]>();

	public ShipStructure (IRoom[] rooms)
	{
		roomList = rooms;
		for (int i = 0; i < roomList.Length; i++)
		{
			Neighbor[] roomNeighbors = new Neighbor[roomList[i].Neighbors.Count];
			roomList[i].Neighbors.CopyTo(roomNeighbors, 0);
			shipGraph.Add(roomList[i], roomNeighbors);
		}
	}

	// Returns list of world points for NPC traveling
	// https://gist.github.com/joninvski/701720
	public List<Vector3> GetStepsToRoom (IRoom initialRoom, IRoom targetRoom) {
		// Initializing
		Dictionary<IRoom, float> distances = new Dictionary<IRoom, float>();
		Dictionary<IRoom, IRoom> predecessors = new Dictionary<IRoom, IRoom>();

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

	private List<Vector3> RestoreClosesPath (IRoom tRoom, Dictionary<IRoom, IRoom> predecessors)
	{
		List<Vector3> result = new List<Vector3>();
		IRoom stepRoom = tRoom;
		IRoom nextStepRoom;
//		result.Add (tRoom.transform.position);
		while (stepRoom != null)
		{
			nextStepRoom = predecessors[stepRoom];
			if (nextStepRoom != null) 
			{ 
				Neighbor nextNeighbor = stepRoom.GetNeighbor (nextStepRoom);
				result.Add (nextNeighbor.ExitPoint);
				result.Add (nextNeighbor.EntrancePoint);
			}
			stepRoom = nextStepRoom;
		}
		
		result.Reverse();
		return result;
	}

}
