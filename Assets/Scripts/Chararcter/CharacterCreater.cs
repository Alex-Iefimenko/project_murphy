using UnityEngine;
using System.Collections;

public class CharacterCreater : MonoBehaviour {

	public Enums.CharacterSides characterSide;
	public Enums.CharacterTypes characterType;
	public GameObject[] prefabs;

	public void CreateCharacter ()
	{
		GameObject character = (GameObject)Instantiate(Helpers.GetRandomArrayValue(prefabs), 
		                                               transform.position, 
		                                               transform.rotation);
		CharacterMain newCharacter = character.GetComponent<CharacterMain>();
		newCharacter.characterSide = characterSide;
		newCharacter.characterType = characterType;
		newCharacter.Init();
		Destroy(this.gameObject);
	}

}
