using UnityEngine;
using System.Collections;
using E = Enums;
using LitJson;

public class CharacterFactory
{
	public static IGroupCharacter CreateCharacter (GameObject characterObj, E.CharacterSides side, E.CharacterTypes type)
	{
		ICharacterStatePrivate stats = CreateCharacterStats (characterObj, side, type);
		IMovement movement = CreateCharacterMovement (characterObj, stats);
		IndividualCoordinator coordinator = new IndividualCoordinator ();
		AiStateParams param = new AiStateParams(stats, movement, coordinator);
		ICharacterAIHandler aiHandler = new CharacterAIHandler(CreateAIPriorities (side, type), param);
		ICharacterView view = CreateCharacterView (characterObj, side, type);
		new ViewObserver (view, aiHandler, movement);
		IGroupCharacter character = CreateFasade (characterObj, aiHandler, param);
		return character;
	}

	private static ICharacterStatePrivate CreateCharacterStats (GameObject characterObj, E.CharacterSides side, E.CharacterTypes type)
	{
		string statsType = side.ToString() + type.ToString() + "Stats";
		CharacterStatsBase stats = characterObj.AddComponent(statsType) as CharacterStatsBase;
		stats.Init ();
		return (ICharacterStatePrivate)stats;
	}

	private static Movement CreateCharacterMovement (GameObject characterObj, ICharacterStatePrivate stats)
	{
		Movement movement = characterObj.AddComponent<Movement>() as Movement;
		movement.Init (stats.WalkSpeed, stats.RunSpeed);
		return movement;
	}

	public static string[] CreateAIPriorities (E.CharacterSides side, E.CharacterTypes type)
	{
		string path = "Characters/Data/AI/" + side.ToString() + "/" + type.ToString();
		TextAsset json = Resources.Load(path) as TextAsset;
		JsonData aiContainer = JsonMapper.ToObject(json.text);
		var aiArray = aiContainer["AIFlow"];
		string[] priorities = new string[aiArray.Count];
		for (int i = 0; i < aiArray.Count; i++ ) priorities[i] = aiArray[i].ToString();
		return priorities; 
	}

	private static CharacterView CreateCharacterView (GameObject characterObj, E.CharacterSides side, E.CharacterTypes type)
	{
		string controllerPath = "Characters/Controllers/" + side.ToString() + "/" + type.ToString() + "/Controller";
		RuntimeAnimatorController controller = Resources.Load (controllerPath) as RuntimeAnimatorController;
		return new CharacterView (characterObj, controller);
	}

	private static IGroupCharacter CreateFasade (GameObject characterObj, ICharacterAIHandler aiHandler, AiStateParams param)
	{
		IGroupCharacter character = characterObj.AddComponent<Character>() as IGroupCharacter;
		character.Init (aiHandler, param);
		return character;
	}
}
