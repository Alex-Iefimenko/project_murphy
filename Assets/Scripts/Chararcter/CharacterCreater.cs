using UnityEngine;
using System.Collections;

public class CharacterCreater : MonoBehaviour {

	public Enums.CharacterSides characterSide;
	public Enums.CharacterTypes characterType;
	public GameObject[] prefabs;

	public CharacterMain CreateCharacter ()
	{
		Vector3 place = new Vector3(transform.position.x, transform.position.y, -0.2f);
		GameObject character = (GameObject)Instantiate(Helpers.GetRandomArrayValue(prefabs), 
		                                               place, 
		                                               transform.rotation);
		CharacterMain newCharacter = character.GetComponent<CharacterMain>();
		newCharacter.characterSide = characterSide;
		newCharacter.characterType = characterType;
		newCharacter.Init();
		Destroy(this.gameObject);
		return newCharacter;
	}

}
