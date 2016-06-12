using UnityEngine;
using System.Collections;
using System.Linq;

public class RoomStatesHandler {

	private IRoomController controller;
	private RoomStateBase[] states;
	private GameObject[] warnings;
	private Vector3 initialPoint;
	private float interval;

	public RoomStatesHandler (IRoomController roomController, IRoomStats stats, IRoomObjectTracker objects) 
	{
		controller = roomController;
		states = controller.GObject.GetComponentsInChildren<RoomStateBase>();
		for (int i = 0; i < states.Length; i++) states[i].Init (controller, stats, objects);
		Broadcaster.Instance.tickEvent += DetectState;
		warnings = new GameObject[states.Length];
		Warning firstWarning = controller.GObject.GetComponentInChildren<Warning>();
		Bounds warningBounds = firstWarning == null ? new Bounds(Vector3.zero, Vector3.zero) : firstWarning.renderer.bounds;
		Bounds roomBounds = controller.RoomBounds;
		initialPoint = new Vector3 (roomBounds.max.x - warningBounds.extents.x, roomBounds.min.y + warningBounds.extents.y);
		interval = warningBounds.size.x / 3f;
	}

	public void DetectState ()
	{
		for (int i = 0; i < states.Length; i++) 
		{
			states[i].AutoEnable();
			warnings[i] = states[i].Enabled ? states[i].Warning : null;
		}
		DisplayWarnings ();
	}

	public bool ForceState<T> (float amount) where T : RoomStateBase
	{
		T state = controller.GObject.GetComponentInChildren<T>();
		return state == null ? false : state.InitiatedEnable(amount);
	}

	private void DisplayWarnings ()
	{
		int warnCount = warnings.Count(s => s != null);
		float expectedLength = interval * warnCount;
		float actualInterval = interval;
		if (controller.RoomBounds.size.x < expectedLength) actualInterval = controller.RoomBounds.size.x / warnCount;
		int place = 0;
		for (int i = 0; i < warnings.Length; i++)
		{
			if (warnings[i] != null) 
			{
				warnings[i].transform.position = initialPoint - place * new Vector3(actualInterval, 0);
				place++;
			}
		}
	}

}
