using UnityEngine;
using System.Collections;

public class RoomStatesHandler {

	private Room room;
	private RoomStateBase[] states;

	public RoomStatesHandler (Room currentRoom) 
	{
		room = currentRoom;
		states = room.gameObject.GetComponentsInChildren<RoomStateBase>();
		Broadcaster.Instance.tickEvent += DetectState;
	}

	public void DetectState ()
	{
		for (int i = 0; i < states.Length; i++) states[i].AutoEnable();
	}

	public bool ForceState<T> (float amount) where T : RoomStateBase
	{
		T state = room.gameObject.GetComponentInChildren<T>();
//		return state == null ? false : state.InitiatedEnable(amount);
		return state.InitiatedEnable(amount);
	}

}
