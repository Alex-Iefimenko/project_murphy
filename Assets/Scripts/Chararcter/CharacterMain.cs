using UnityEngine;
using System.Collections;
using E = Enums;

public class CharacterMain : MonoBehaviour, ICharacter, IMovable {

	//Side
	public E.CharacterSides characterSide;
	public E.CharacterSides Side { get { return characterSide; } }

	//Profession
	public E.CharacterTypes characterType;
	public E.CharacterTypes Type { get { return characterType; } }

	// Character Components
	public CharacterStatsBase Stats { get; private set; }
	public ICharacterAIHandler AiHandler { get; private set; }
	public IMovement Movement { get; private set; }
	public ICharacterView View { get; private set; }
	public Coordinator Coordinator { get; set; }

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
		Component[] sprites = GetComponentsInChildren(typeof(SpriteRenderer), true);
		int layer = Random.Range(100, 32766);
		foreach (SpriteRenderer sprite in sprites) sprite.sortingOrder = layer;
	}

	public void MuatateReaction (string currentReaction, string newReaction)
	{
		AiHandler.ChangeReaction (currentReaction, newReaction);
	}

	public void MutateType (E.CharacterTypes newType)
	{
		characterType = newType;
		AiHandler = CharacterAIGenerator.GenerateAIHandler(this);
	}

	public void MutateSide (E.CharacterSides newSide)
	{
		characterSide = newSide;
	}

	public void MutateSideAndType (Enums.CharacterSides newSide, Enums.CharacterTypes newType)
	{
		MutateSide (newSide);
		MutateType (newType);
	}

	public void MutateFully (Enums.CharacterSides newSide, Enums.CharacterTypes newType)
	{
		// Not Implemented
		MutateSideAndType (newSide, newType);
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
	public void Navigate (Room room, bool full)
	{
		if (Stats.IsActive())
		{
			NavigateState state = AiHandler.GetState<NavigateState>();
			state.TargetRoom = room;
			state.Full = full;
			AiHandler.ForceState(state);
		}
	}

	public void Heal (float amount)
	{
		Stats.HealthReduction = 0f;
		Stats.Health += amount;
	}

	public void Hurt (float amount)
	{
		Stats.Health -= amount;
	}

}
