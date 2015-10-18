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
	public ICharacterView CharacterView { get; private set; }

	// Initialize method
	void Start () 
	{
		Stats = CharacterStatsGenerator.GenerateCharacterStats(this);
		Movement = (IMovement)gameObject.AddComponent("Movement");
		AiHandler = CharacterAIGenerator.GenerateAIHandler(this);
		CharacterView = (ICharacterView)gameObject.AddComponent("CharacterView");
	}



}
