using UnityEngine;
using System.Collections;

public class RoomMotion : MonoBehaviour, IRoomMotion {

	private float speed = 2.5f;
	private Transform dockingPoint;
	private delegate void FlyEnd();
	private FlyEnd flyEnd;
	private IRoomObjectTracker roomObjects;

	public event RoomExternalPartsHandler OnRoofChange;
	public event RoomExternalPartsHandler OnEnginesChange;
	public Beacon Gateway { get; set; }

	void Awake ()
	{
		DockPoint dockingObject = GetComponentInChildren<DockPoint>();
		dockingPoint = dockingObject == null ? this.transform : dockingObject.transform;
	}

	public void Init (IRoomObjectTracker objectTracker)
	{
		roomObjects = objectTracker;
	}

	public void ChangeRoof (bool swch)
	{
		if (OnRoofChange != null) OnRoofChange (swch);
	}

	public void FlyUp (Vector3 point)
	{
		if (OnRoofChange != null) OnRoofChange (true);
		if (OnEnginesChange != null) OnEnginesChange (true);
		flyEnd = Dock;
		StartCoroutine(RunTransition(point));
	}

	public void FlyAway (Vector3 point)
	{
		if (OnRoofChange != null) OnRoofChange (true);
		if (OnEnginesChange != null) OnEnginesChange (true);
		System.Collections.Generic.List<ICharacter> characters = roomObjects.Characters;
		for (int i = 0; i < characters.Count; i++) characters[i].GObject.transform.parent = transform;
		flyEnd = Vanish;
		StartCoroutine(RunTransition(point));
	}

	private IEnumerator RunTransition (Vector3 endPoint)
	{
		Vector3 point = endPoint - dockingPoint.position + transform.position;
		while (transform.position != point)
		{
			transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
			yield return null;
		}
		flyEnd();
	}

	private void Dock()
	{
		ShipState.Inst.Init();
		CharacterCreater[] characters = GetComponentsInChildren<CharacterCreater>();
		for (int i = 0; i < characters.Length; i++) characters[i].CreateCharacter();
		CharacterGroupCreater[] characterGroups = GetComponentsInChildren<CharacterGroupCreater>();
		for (int i = 0; i < characterGroups.Length; i++) characterGroups[i].CreateCharacters();
		ShipState.Inst.CountCharacters();
		if (OnRoofChange != null) OnRoofChange (false);
		if (OnEnginesChange != null) OnEnginesChange (false);
		if (Gateway) Gateway.OnDock();
	}

	private void Vanish ()
	{
		Destroy(gameObject);
		if (Gateway) Gateway.OnUnDock();
	}
}
