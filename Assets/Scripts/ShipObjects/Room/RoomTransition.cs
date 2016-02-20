using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour {

	private float speed = 2.5f;
	private Engines engines;
	private Roof roof;
	private Transform dockingObject;
	private delegate void FlyEnd();
	private FlyEnd flyEnd;

	public Beacon Gateway { get; set; }

	void Awake ()
	{
		roof = GetComponentInChildren<Roof>();
		engines = GetComponentInChildren<Engines>();
		DockPoint dockingPoint = GetComponentInChildren<DockPoint>();
		dockingObject = dockingPoint == null ? this.transform : dockingPoint.transform;
	}

	public void FlyUp (Vector3 point)
	{
		engines.SwitchOn ();
		flyEnd = Dock;
		StartCoroutine(RunTransition(point));
	}

	public void FlyAway (Vector3 point)
	{
		HideRoof();
		roof = null;
		if (engines) engines.SwitchOn();
		System.Collections.Generic.List<ICharacter> characters = GetComponent<Room>().Objects.Characters;
		for (int i = 0; i < characters.Count; i++) characters[i].GObject.transform.parent = transform;
		flyEnd = Vanish;
		StartCoroutine(RunTransition(point));
	}

	public void ShowRoof ()
	{
		if (roof) roof.ShowRoof();
	}

	public void HideRoof ()
	{
		if (roof) roof.HideRoof();
	}

	private IEnumerator RunTransition (Vector3 endPoint)
	{
		Vector3 point = endPoint - dockingObject.position + transform.position;
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
		ShowRoof();
		if (engines) engines.SwitchOff();
		if (Gateway) Gateway.OnDock();
	}

	private void Vanish ()
	{
		Destroy(gameObject);
		if (Gateway) Gateway.OnUnDock();
	}
}
