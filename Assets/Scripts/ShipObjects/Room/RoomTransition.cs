using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour {

	private float speed = 2.5f;

	public void Fly (Vector3 point, bool activate)
	{
		StartCoroutine(RunTransition(point));
	}

	private IEnumerator RunTransition (Vector3 endPoint)
	{
		while(transform.position != endPoint)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
			yield return null;
		}
		ShipState.Inst.Init();
		CharacterCreater[] characters = GetComponentsInChildren<CharacterCreater>();
		for (int i = 0; i < characters.Length; i++) characters[i].CreateCharacter();
		ShipState.Inst.CountCharacters();
	}
}
