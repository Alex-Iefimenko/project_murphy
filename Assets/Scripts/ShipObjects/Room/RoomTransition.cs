using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour {

	private float speed = 2.5f;
	private delegate void FlyEnd();
	private FlyEnd flyEnd;

	public void FlyUp (Vector3 point)
	{
		flyEnd = Dock;
		StartCoroutine(RunTransition(point));
	}

	public void FlyAway (Vector3 point)
	{
		System.Collections.Generic.List<ICharacter> characters = GetComponent<Room>().Objects.Characters;
		for (int i = 0; i < characters.Count; i++) characters[i].GObject.transform.parent = transform;
		flyEnd = Vanish;
		StartCoroutine(RunTransition(point));
	}

	private IEnumerator RunTransition (Vector3 endPoint)
	{
		while (transform.position != endPoint)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
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
	}

	private void Vanish ()
	{
		Destroy(gameObject);
	}
}
