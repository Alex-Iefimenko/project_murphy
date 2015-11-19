using UnityEngine;
using System.Collections;
using LitJson;

static public class CharacterAIGenerator {

	private static JsonData GetAIContainer (string side, string type) 
	{
		TextAsset json = Resources.Load("Characters/Data/AI/" + side + "/" + type) as TextAsset;
		return JsonMapper.ToObject(json.text);
	}

	public static CharacterAIHandler GenerateAIHandler (CharacterMain character)
	{
		JsonData aiContainer = GetAIContainer(character.Side.ToString(), character.Type.ToString());
		var aiArray = aiContainer["AIFlow"];
		string[] priorities = new string[aiArray.Count];
		for (int i = 0; i < aiArray.Count; i++ ) priorities[i] = aiArray[i].ToString();
		return new CharacterAIHandler(character, priorities);
	}

}
