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

	// GameObject
	public GameObject GObject { get { return this.gameObject; } }
	public bool Lock { get; set; }

	// Initialize method
	public void Init ()
	{
		Stats = CharacterStatsGenerator.GenerateCharacterStats(this);
		Movement = (IMovement)gameObject.AddComponent("Movement");
		AiHandler = CharacterAIGenerator.GenerateAIHandler(this);
		View = (ICharacterView)gameObject.AddComponent("CharacterView");
		Broadcaster.Instance.tickEvent += Tick;
	}

	public void PurgeActions ()
	{
		AiHandler.Purge();
		Movement.Purge();
		View.Purge();
		Stats.Purge();
	}

	public void Tick ()
	{
		Stats.StatsUpdate();
		AiHandler.React();
	}

	//
	// Facade implementations
	//
	public void Heal(float amount)
	{
		Stats.Health += amount;
	}

	public void Navigate(Room room, bool full)
	{
		AiHandler.ForceState<NavigateState>();
		Movement.Navigate(room, full);
	}

	public void Hurt (float amount)
	{
		Stats.Health -= amount;
	}

}
