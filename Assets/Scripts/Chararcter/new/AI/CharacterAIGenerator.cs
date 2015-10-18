using UnityEngine;
using System.Collections;
using LitJson;

static public class CharacterAIGenerator {

	private static JsonData GetAIContainer (string side) 
	{
		TextAsset json = Resources.Load("Characters/Data/AI/" + side + "AIFlows") as TextAsset;
		return JsonMapper.ToObject(json.text);
	}

	public static CharacterAIHandler GenerateAIHandler (CharacterMain character)
	{
		JsonData aiContainer = GetAIContainer(character.Side.ToString());
		var aiArray = aiContainer["AIFlows"][character.Type.ToString()];
		string[] priorities = new string[aiArray.Count];
		for (int i = 0; i < aiArray.Count; i++ ) priorities[i] = aiArray[i].ToString();
		return new CharacterAIHandler(character, priorities);
	}

}
