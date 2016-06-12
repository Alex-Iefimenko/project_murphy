using UnityEngine;
using System.Collections;

public delegate void RoomExternalPartsHandler (bool show); 

public interface IRoomMotion {

	event RoomExternalPartsHandler OnRoofChange;
	event RoomExternalPartsHandler OnEnginesChange;

	void Init (IRoomObjectTracker objectTracker);
	void FlyUp (Vector3 point);
	void FlyAway (Vector3 point);
	void ChangeRoof (bool swch);
	Beacon Gateway { get; set; }
}
