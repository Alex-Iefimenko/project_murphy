using UnityEngine;
using System.Collections;

public class CharacterMain : MonoBehaviour, ICharacter {

	//Side
	public enum CharacterSides{ Crew, Pirate, Trader, Creature, Player };
	public CharacterSides characterSide;
	public CharacterSides Side { get { return characterSide; } }

	//Profession
	public enum CharacterTypes{ Engineer, Safety, Doctor, Scientist, Murphy };
	public CharacterTypes characterType;
	public CharacterTypes Type { get { return characterType; } }

	// Character Components
	public CharacterStatsBase Stats { get; private set; }
	public ICharacterAIHandler AiHandler { get; private set; }
	public IMovement Movement { get; private set; }
	public ICharacterView View { get; private set; }

	// Initialize method
	public void Init ()
	{
		Stats = CharacterStatsGenerator.GenerateCharacterStats(this);
		Movement = (IMovement)gameObject.AddComponent("Movement");
		AiHandler = CharacterAIGenerator.GenerateAIHandler(this);
		View = (ICharacterView)gameObject.AddComponent("CharacterView");
	}

	public void PurgeActions ()
	{
		AiHandler.Purge();
		Movement.Purge();
		View.Purge();
	}


}
