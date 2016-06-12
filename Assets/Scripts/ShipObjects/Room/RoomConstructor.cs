using UnityEngine;
using System.Collections;

public class RoomConstructor : MonoBehaviour {

	public Enums.RoomTypes roomType;
	
	void Awake ()
	{
		RoomFactory.CreateRoom (this);
	}

}
