using UnityEngine;
using System.Collections;
using LitJson;

public static class CharacterStatsGenerator {

	private static JsonData GetStatsConstructor (string side) 
	{
		TextAsset json = Resources.Load("Characters/Data/Stats/" + side + "InitialStats") as TextAsset;
		return JsonMapper.ToObject(json.text);
	}
	
	public static CharacterStatsBase GenerateCharacterStats (CharacterMain character)
	{
		JsonData rawData = GetStatsConstructor(character.Side.ToString())["Stats"][character.Type.ToString()];
		CharacterStatsConstructor contsr = new CharacterStatsConstructor(rawData, character.Type.ToString());
		CharacterStatsBase stats = 
			(CharacterStatsBase)character.gameObject.AddComponent(SidesRelations.Instance.GetStatsType(character.Side));
		stats.Init(contsr);
		return stats;
	}

}
