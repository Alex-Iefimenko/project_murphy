using UnityEngine;
using System.Collections;

public static class CharacterStatsGenerator {

	
	public static CharacterStatsBase GenerateCharacterStats (CharacterMain character)
	{
		string statsType = character.Side.ToString() + character.Type.ToString() + "Stats";
		CharacterStatsBase stats = (CharacterStatsBase)character.gameObject.AddComponent(statsType);
		stats.Init(character);
		return stats;
	}

}
