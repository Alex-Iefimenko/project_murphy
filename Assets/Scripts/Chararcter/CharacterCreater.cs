using UnityEngine;
using System.Collections;

public class CharacterCreater : MonoBehaviour {

	public Enums.CharacterSides characterSide;
	public Enums.CharacterTypes characterType;
	public GameObject[] prefabs;

	public IGroupCharacter CreateCharacter ()
	{
		Vector3 place = new Vector3(transform.position.x, transform.position.y, -0.2f);
		GameObject character = (GameObject)Instantiate(Helpers.GetRandomArrayValue(prefabs), 
		                                               place, 
		                                               transform.rotation);
		IGroupCharacter newCharacter = CharacterFactory.CreateCharacter (character, characterSide, characterType);
		Destroy(this.gameObject);
		return newCharacter;
	}

}
