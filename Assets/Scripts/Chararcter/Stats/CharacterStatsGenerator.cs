using UnityEngine;
using System.Collections;
using LitJson;

public static class CharacterStatsGenerator {

	private static JsonData GetStatsConstructor (string side, string type) 
	{
		TextAsset json = Resources.Load("Characters/Data/Stats/" + side + "/" + type) as TextAsset;
		return JsonMapper.ToObject(json.text);
	}
	
	public static CharacterStatsBase GenerateCharacterStats (CharacterMain character)
	{
		JsonData rawData = GetStatsConstructor(character.Side.ToString(), character.Type.ToString())["Stats"];
		CharacterStatsConstructor contsr = new CharacterStatsConstructor(rawData, character);
		CharacterStatsBase stats = 
			(CharacterStatsBase)character.gameObject.AddComponent(SidesRelations.Instance.GetStatsType(character.Side));
		stats.Init(contsr);
		return stats;
	}

}
